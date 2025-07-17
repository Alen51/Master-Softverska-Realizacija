import React from "react";
import { NavLink, useNavigate } from "react-router-dom";

const NavBar = ({isAuth, tipKorisnika,  handleLogout}) => {

    const active = (isActive) =>{
        if(isActive)
            return "item active"
        else
            return "item"
    }

    

    return (
        <div className="ui inverted blue secondary menu">
        {/*nelogovani i neregistrovani korisnici
        oni vide home, register i login. Posle cu dodavati za role, da li je korisnik ovakav ili onakav*/}
        {isAuth ? null : <NavLink className={({isActive}) => active(isActive)} to="/" >Home page</NavLink> }
        {isAuth ? null : <NavLink className={({isActive}) => active(isActive)} to="/login">Log in</NavLink> }
        {isAuth ? null : <NavLink className={({isActive}) => active(isActive)} to="/registration">Registration</NavLink> }
        
        {/*logovani korisnik koji je admin, dodati proveru za role*/}
        {isAuth && tipKorisnika === 'Administrator' ? <NavLink className={({isActive}) => active(isActive)} to="/adminDashboard">Admin dashboard</NavLink> : null}
        {isAuth && tipKorisnika === 'Administrator' ? <NavLink className={({isActive}) => active(isActive)} to="/adminMap">Admin Map</NavLink> : null}
        {isAuth && tipKorisnika === 'Administrator' ? <NavLink className={({isActive}) => active(isActive)} to="/adminErrors">Admin Errors</NavLink> : null}
        
        {isAuth && tipKorisnika === 'Kupac' ? <NavLink className={({isActive}) => active(isActive)} to="/clientMap">Client Map </NavLink> : null}
        {isAuth && tipKorisnika === 'Kupac' ? <NavLink className={({isActive}) => active(isActive)} to="/clientErrors">Client Errors</NavLink> : null}
        
        {isAuth && tipKorisnika === 'Gost' ? <NavLink className={({isActive}) => active(isActive)} to="/gostMap">gost Map</NavLink> : null}
        

        {isAuth  ? <NavLink className={({isActive}) => active(isActive)} to="/profil">Profil</NavLink> : null}
       
        {isAuth ? <NavLink className={({isActive}) => active(isActive)} onClick={handleLogout} to="/">Logout</NavLink> : null}


    </div>
    )
}


export default NavBar;