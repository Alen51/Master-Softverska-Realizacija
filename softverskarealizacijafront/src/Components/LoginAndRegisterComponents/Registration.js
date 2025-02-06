import React, { useState, useEffect} from "react";
import { useNavigate } from "react-router-dom";

import { RegisterUser } from "../../Services/ClientService";
import jwt_decode from 'jwt-decode';


const Registration = ({handleKorisnikInfo}) => {
    
    const [email, setEmail] = useState('');
    const [password, setLozinka] = useState('');
    const [password2, setLozinka2] = useState('');
    const [fullName, setIme] = useState('');
    
    const [tipKorisnika, setTipKorisnika] = useState('Kupac');
    
    const navigate = useNavigate();


    const[error, setError] = useState(false);


    

    //verifikacija korisnika preko gmaila
    /*
    useEffect(() => {
        google.accounts.id.initialize({
            client_id: process.env.REACT_APP_GOOGLE_CLIENT,
            callback: handleCallbackResponse
        });

        google.accounts.id.renderButton(
            document.getElementById('signInDiv'),
            {theme: "outline", size:"medium"}
        )
    }, [])*/

    

    const setInputsToEmpty = () => {
       
        setEmail('');
        setLozinka('');
        setLozinka2('');
        setIme('');
        
        setTipKorisnika('Kupac');
        
    }

    const redirectTo = (tipKorisnika) => {
        if(tipKorisnika === 'Admin'){
            navigate('/adminDashboard');
        }
        else if(tipKorisnika === 'Kupac'){
            navigate('/kupacDashboard');
        }
        else if(tipKorisnika === 'Gost'){
            navigate('/gostDashboard');
        }
    }


    const handleSubmit = async (event) => {
        event.preventDefault();
      
        //uraditi provere za lozinke, tj. da li se prva i druga poklapaju i da li su uneta stva polja

        if(email.length === 0 || password.length === 0 || password2.length === 0 
            || fullName.length === 0 ||  password !== password2 ){
                setError(true);
                return;
            }


        if(password === password2){
            const korisnikJSON = JSON.stringify({
                email,
                password,
                fullName, 
                tipKorisnika});


            const data = await RegisterUser(korisnikJSON);
            if(data !== null){
                sessionStorage.setItem('isAuth', JSON.stringify(true));
                sessionStorage.setItem('token', data.token)
                sessionStorage.setItem('korisnik', JSON.stringify(data.korisnikDto));
                handleKorisnikInfo(true); //prvo se postave podaci pa se re reneruje
                alert("Uspesno ste se registrovali");
                redirectTo('/profil');

            } else {
                setInputsToEmpty();
                sessionStorage.setItem('isAuth', JSON.stringify(false));
                handleKorisnikInfo(false);
            }
        }
    }

    return (
      <div className="card">
            <form className="ui form" onSubmit={handleSubmit}>
            <h2 className="ui centered aligned header">Registration</h2>
                      
               
                <div className="field">
                    <label>Email</label>
                    <input type="email"
                           name="email" 
                           placeholder="Email"
                           value={email}
                           onChange={(e) => setEmail(e.target.value)}/>
                    {error && email.length === 0 ? <div className="ui pointing red basic label">Morate uneti email</div> : null}
                </div>
                <div className="field">
                    <div className="two fields"> 
                        <div className="field">
                            <label>Lozinka</label>
                            <input  type="password" 
                                    name="password" 
                                    placeholder="Password"
                                    value={password}
                                    onChange={(e) => setLozinka(e.target.value)}/>
                            {error && password.length === 0 ? <div className="ui pointing red basic label">Morate uneti lozinku</div> : null}
                        </div>
                        <div className="field">
                            <label>Potvrdite lozinku</label>
                            <input  type="password" 
                                    name="password2" 
                                    placeholder="Potvrdite lozinku"
                                    value={password2}
                                    onChange={(e) => setLozinka2(e.target.value)}
                                    />
                            {error && password2.length === 0 ? <div className="ui pointing red basic label">Morate potvrditi lozinku</div> : null}
                        </div>
                    </div>
                </div>
                    {error && password !== password2  ? 
                        <div className="field">
                            <div className="ui pointing red basic label">Lozinke se moraju poklapati</div>
                        </div>
                    : null}
                <div className="field">
                    <div className="two fields">
                        <div className="field">
                            <label>Ime i prezime</label>
                            <input  type="text"
                                    name="fullName" 
                                    placeholder="FullName"
                                    value={fullName}
                                    onChange={(e) => setIme(e.target.value)}
                                    />
                            {error && fullName.length === 0 ? <div className="ui pointing red basic label">Morate uneti ime i prezime</div> : null}
                        </div>
                        
                    </div>
                </div>
                <div className="fields">
                    
                    <div className="ten wide field">
                        <label>Tip korisnika</label>
                        <select value={tipKorisnika} className="ui fluid dropdown" onChange={(e) => setTipKorisnika(e.target.value)}>
                            <option value="Kupac">Kupac</option>
                            
                            <option value="Administrator">Administrator</option>
                        </select>
                    </div>
                </div>
                
                
                        
                <div className="buttons-flex">
                    <button className="ui blue button" type="submit">Submit</button>
                    <div id="signInDiv"></div>
                </div>
                
                
               
            </form>
        </div>
    );
}

export default Registration;