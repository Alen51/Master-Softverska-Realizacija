import React, { useState, useEffect } from "react";
import "leaflet/dist/leaflet.css";
import {MapContainer, TileLayer, Marker, Polyline,Popup, useMapEvents} from "react-leaflet" 
import { Icon, divIcon, point } from "leaflet";
import { GetNodeConnections, GetNodes , getKvarList, addKvar } from "../../Services/MapService";
import { useNavigate } from "react-router-dom";



const ClientMap = () =>{

    const [nodes,setNodes] = useState([]);
    const [loading, setLoading] = useState(true);
    const [lines, setLines] = useState([]);
    const [errors, setErrors] = useState([]);
    const [korisnikId, setKorisnikId]= useState([]);

    const customIcon = new Icon({
        
        // iconUrl: "https://cdn-icons-png.flaticon.com/512/447/447031.png",
        iconUrl: require("../../Icons/placeholder.png"),
        iconSize: [38, 38] // size of the icon

    });

    
    useEffect(() => {
        const getAuth = () => {
            if(sessionStorage.getItem('korisnik') !== null){
                
                const korisnik = JSON.parse(sessionStorage.getItem('korisnik'))
                setKorisnikId(korisnik.id);
               
            }
        }
        getAuth();
      });


    const reportProblem = async (pinId) => {

        try {

            //logika o dodavanju kvara
            const kvarJSON=JSON.stringify({vremmePrijave: new Date().toISOString() , vremeOtklanjanja:null , client:korisnikId, node:pinId, stanjeKvara:"Aktivan" })
            const newError = addKvar(kvarJSON);
            console.log("New error:",newError);
            //setErrors([...errors, newError]);
            alert(`Reported problem for pin ID: ${pinId}`);

        } catch (error) {
            console.error('Failed to report problem:', error);
        }

    };

    const getNodes = async () => {

        setNodes([]);
        setLoading(true);

        const data = await GetNodes();
        
        if(data !== null){
            setNodes(data);
            
            setLoading(false);
            console.log("Nodes:",nodes);
        }

    }

    const getLines = async () => {

        setLoading(true);
        setLines([]);
        const data = await GetNodeConnections();
        
        if(data !== null){
            setLines(data);
            
            setLoading(false);
            console.log("Lines:",lines);
        }

    }

    const getErrors = async () => {
        
        setLoading(true);
        setErrors([]);
        const data = await getKvarList();
        
        if(data !== null){
            setErrors(data);
            
            setLoading(false);
            console.log("Errors:",errors);
        }

    }

    const handlePrikazDugmadi = (node) => {

        if(errors.find(e=>e.node === node.id && e.client=== korisnikId && e.stanjeKvara == "Aktivan") ){
            return  null;
        }
        else {
            return    <button className="ui red button" onClick={() => reportProblem(node.id)}>Report Problem</button>
                        
        }
    }

    const handleLine= (line,startPin,endPin)=>{
        if(line.hasError){
            return <Polyline
                        key={line.id}
                        positions={[
                        [startPin.latitude, startPin.longitude],
                        [endPin.latitude, endPin.longitude],
                        ]}
                        
                        color="black"
                    />
        }
        else{
            return <Polyline
                        key={line.id}
                        positions={[
                        [startPin.latitude, startPin.longitude],
                        [endPin.latitude, endPin.longitude],
                        ]}
                        
                        color="blue"
                    />
        }
    }

    const getMpaData = () =>{

        getNodes();
        getLines();
        getErrors();

    }
      
   return(
    <div>
        <h1>Client Map</h1>
        <div className="buttons-flex">
            
            <button className="ui blue button" onClick={() => getMpaData()}>
            Map data
            </button>
            



        </div>
        <div></div>
        <div className="test">
            <MapContainer center={[45.2396,19.8227]} zoom={15}>
                <TileLayer
                    attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
                    url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                />
                
                {nodes.map((node)=>(
                    <Marker key={node.id} position={[node.latitude, node.longitude]} icon={customIcon} 
                            
                    >
                    
                    
            
                    <Popup>
                        <div>
                        <p><strong>Pin ID:</strong> {node.id}</p>
                        {handlePrikazDugmadi(node)}
                        </div>
                    </Popup>
                    
                    </Marker>
                    
                    
                ))}

                {lines.map((line) => {
                    
                    const startPin = nodes.find(p => p.id === line.startPinId);
                    const endPin = nodes.find(p => p.id === line.endPinId);

                    return startPin && endPin ? (
                    handleLine(line,startPin,endPin)
                    ) : null;

                })}
            </MapContainer>
            </div>

                                        
                                        
    </div>
   );
}

export default ClientMap;