import React, { useState, useEffect } from "react";
import "leaflet/dist/leaflet.css";
import {MapContainer, TileLayer, Marker, Polyline,Popup, useMapEvents} from "react-leaflet" 
import { Icon, divIcon, point } from "leaflet";
import { getMapInfo, addNode, addNodeConnection } from "../Services/MapService";
import { useNavigate } from "react-router-dom";

const Home = () => {
  return(
    <>
        <h1 color="red" className="ui blue center aligned header">

            Dobrodosli na stranicu
            
        </h1>
    </>
);
}


export default Home;