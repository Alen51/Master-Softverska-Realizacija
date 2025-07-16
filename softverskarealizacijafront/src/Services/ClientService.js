import axios from "../api/axios";
import ResponseDto from "../Models/ResponseDto.js";
import ClientDto from "../Models/ClientDto.js";


export const LoginUser = async (loginJSON) => {
  const LOGIN_URL = "/users/login";
  var msgl;
  try {
    const { data } = await axios.post(
       `${process.env.REACT_APP_API_BACK}${LOGIN_URL}`,
      loginJSON,
      {
        headers: { "Content-Type": "application/json" },
        withCredentials: true,
      }
    );
    msgl=data;
    console.log(data)
    const response = new ResponseDto(data);
    return response;

  } catch (err) {
    alert("Error while login:"+msgl);
    return null;
  }
};

export const RegisterUser = async (clientJSON) => {
    const REGISTRATION_URL = "/users/registration";
    var msg1;
    try{
       const {data} = await axios.post(`${process.env.REACT_APP_API_BACK}${REGISTRATION_URL}`,
            clientJSON,
            {
                headers:{'Content-Type' : 'application/json'},
                withCredentials: true
            }
        );
        console.log(data);
        const response = new ResponseDto(data);
        msg1=response.result;
        return response;
    }catch(err){
        alert("Nesto se desilo prilikom registracije"+ msg1);
        
        return null;
    }
};

export const EditProfile = async (updatedKorisnikJSON, id, token) => {
  const UPDATE_URL = "/users/" + id ; //treba da dobije id isto
  try{
      const {data} = await axios.put(
          `${process.env.REACT_APP_API_BACK}${UPDATE_URL}`,
          updatedKorisnikJSON,
          {
              headers: 
              {
                  'Content-Type' : 'application/json',
                  'Authorization' : token
              },
              withCredentials: true
          }
      );
      const updatedKorisnik = new ClientDto(data);
      return updatedKorisnik;
  }catch(err){
      console.log(err);
      alert("Error while profile data edit")
  }
};

export const GetAllUsers = async () => {
  const GET_USERS_URL = '/users/getAll'
  try{
      const {data} = await axios.get(
          `${process.env.REACT_APP_API_BACK}${GET_USERS_URL}`,
          {
              headers:{
                  'Content-Type' : 'application/json',
                  
              },
          }
      );
      const korisnici = data.map(korisnik => {
          return new ClientDto(korisnik);
      })
      return korisnici;
  }catch(err){
      console.log(err);
      alert("Nesto se desilo prilikom dobavljanja prodavaca");
      return null;
  }
};