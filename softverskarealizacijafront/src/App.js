import logo from './logo.svg';
import './App.css';

import Home from './Components/Home';
import NavBar from './Components/NavBar';
import Login from './Components/LoginAndRegisterComponents/Login';
import Registration from './Components/LoginAndRegisterComponents/Registration';
import AdminDashboard from './Components/AdministratorComponents/AdminDashboard';
import AdminMap from './Components/AdministratorComponents/AdminMap';
import Profil from './Components/ProfileComponents/Profile';
import { Route, Routes } from 'react-router-dom';
import { useEffect, useState } from 'react';


function App() {

  const [isAuth, setIsAuth] = useState(false);
  const [tipKorisnika, setTipKorisnika] = useState('');
  
  const [isKorisnikInfoGot, setIsKorisnikInfoGot] = useState(false);  //ovo govori da li smo dobili podatke o korisniku
  
  useEffect(() => {
    const getAuth = () => {
        if(sessionStorage.getItem('korisnik') !== null && sessionStorage.getItem('isAuth') !== null){
            setIsAuth(JSON.parse(sessionStorage.getItem('isAuth')))
            const korisnik = JSON.parse(sessionStorage.getItem('korisnik'))
            setTipKorisnika(korisnik.tipKorisnika);
           
        }
    }
    getAuth();
  }, [isKorisnikInfoGot]);

  const handleKorisnikInfo = (gotKorisnikInfo) => {
    setIsKorisnikInfoGot(gotKorisnikInfo);
  }

  const handleLogout = () => {
    sessionStorage.removeItem('korisnik');
    sessionStorage.removeItem('isAuth');
    sessionStorage.removeItem('token');
    setIsAuth(false);
    
    setTipKorisnika('');
    setIsKorisnikInfoGot(false);  
  
    
  }

  const routes = [
    {path: '/', element: <Home></Home>},
    {path: '/login', element: <Login handleKorisnikInfo={handleKorisnikInfo}></Login>},
    {path: '/registration', element: <Registration handleKorisnikInfo={handleKorisnikInfo}></Registration>},
    {path: '/adminDashboard', element: <AdminDashboard></AdminDashboard>},
    {path: '/adminMap', element: <AdminMap></AdminMap>},
    {path: '/profil', element: <Profil></Profil>}
    
  ]

  return (
    <div className='App'>
     
    <NavBar isAuth={isAuth} tipKorisnika = {tipKorisnika} handleLogout={handleLogout}/>
    <div className='container'>
      <Routes>
        {
          routes.map((route) => (
            <Route path={route.path} element={route.element}></Route>
          ))
        }
      </Routes>
      
    </div>
    
  </div>
  );
}

export default App;
