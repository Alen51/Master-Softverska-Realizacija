import React, { useState } from "react";
import { useNavigate } from "react-router-dom";

import {EditProfile} from "../../Services/ClientService";


const Profil = () => {
  const korisnik = JSON.parse(sessionStorage.getItem("korisnik"));


  const id = korisnik.id;
  const tipKorisnika = korisnik.tipKorisnika;
  
  
  const [email, setEmail] = useState(korisnik.email);
  const [password, setLozinka] = useState(korisnik.password);
  const [fullName, setIme] = useState(korisnik.fullName);
  
  const navigate = useNavigate();

  const [error, setError] = useState(false);

  const redirectTo = (tipKorisnika) => {
    if (tipKorisnika === "Admin") {
      navigate("/adminDashboard");
    } else if (tipKorisnika === "Kupac") {
      navigate("/kupacDashboard");
    } else if (tipKorisnika === "Gost") {
      navigate("/gostDashboard");
    }
  };


  

  const handleSubmit = async (event) => {
    event.preventDefault();

    if (
      
      email.length === 0 ||
      password.length === 0 ||
      fullName.length === 0 
    ) {
      setError(true);
      return;
    }

    const updatedKorisnikJSON = JSON.stringify({
      
      email,
      password,
      fullName,
      
      tipKorisnika,
      
    });

    const token = sessionStorage.getItem("token");

    const data = await EditProfile(updatedKorisnikJSON, id, token)
    if (data !== null) {
      sessionStorage.setItem("korisnik", JSON.stringify(data));
      alert("Sucsesfully updated profile data");
      redirectTo(tipKorisnika);
    }
  };

  return (
    <div className="card">
      <form className="ui form" onSubmit={handleSubmit}>
        <h2 className="ui center aligned header">Izmenite Profil</h2>
        
        <div className="field">
          
        </div>
        <div className="field">
          <label>Email</label>
          <input
            disabled
            type="email"
            name="email"
            placeholder="Email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
          {error && email.length === 0 ? (
            <div className="ui pointing red basic label">
              Morate uneti email
            </div>
          ) : null}
        </div>
        <div className="field">
          <label>Password</label>
          <input
            type="password"
            name="password"
            placeholder="Password"
            value={password}
            onChange={(e) => setLozinka(e.target.value)}
          />
          {error && password.length === 0 ? (
            <div className="ui pointing red basic label">
              Morate uneti password
            </div>
          ) : null}
        </div>
        <div className="field">
          <div className="two fields">
            <div className="field">
              <label>FullName</label>
              <input
                type="text"
                name="fullName"
                placeholder="FullName"
                value={fullName}
                onChange={(e) => setIme(e.target.value)}
              />
              {error && fullName.length === 0 ? (
                <div className="ui pointing red basic label">
                  Morate uneti puno ime i prezime
                </div>
              ) : null}
            </div>
            
          </div>
        </div>
        
        <button className="ui blue button" type="submit">
          Submit
        </button>
      </form>
    </div>
  );
};

export default Profil;