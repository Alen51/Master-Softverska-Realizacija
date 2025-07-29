import React, { useState, useEffect } from "react";
import "leaflet/dist/leaflet.css";
import {MapContainer, TileLayer, Marker, Polyline,Popup, useMapEvents} from "react-leaflet" 
import { Icon, divIcon, point } from "leaflet";
import {  GetNodeConnections, GetNodes,  getKvarList } from "../../Services/MapService";

const GostMap = () =>{

    const [nodes,setNodes] = useState([]);
    const [loading, setLoading] = useState(true);
    const [lines, setLines] = useState([]);
    const [errors, setErrors] = useState([]);

    const customIcon = new Icon({
                // iconUrl: "https://cdn-icons-png.flaticon.com/512/447/447031.png",
        iconUrl: require("../../Icons/placeholder.png"),
        iconSize: [38, 38] // size of the icon
    });

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


return (
        <div>
            <h1 color="red" className="ui blue center aligned header">

            Dobrodosli na stranicu

            </h1>
           
              
              
              
            
            <div className="buttons-flex">
                            
                            <button className="ui blue button" onClick={() => getNodes()}>
                                Get Nodes
                            </button>
                            <button className="ui blue button" onClick={() => getLines()}>
                                Get Lines
                            </button>
                            <button className="ui blue button" onClick={() => getErrors()}>
                                Get Errors
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
                                        </div>
                                    </Popup>
                                    
                                  </Marker>
                                  
                                  
                                ))}
                
                                {lines.map((line) => {
                                  const startPin = nodes.find(p => p.id === line.startPinId);
                                  const endPin = nodes.find(p => p.id === line.endPinId);
                                  return startPin && endPin ? (
                                    <Polyline
                                      key={line.id}
                                      positions={[
                                        [startPin.latitude, startPin.longitude],
                                        [endPin.latitude, endPin.longitude],
                                      ]}
                                      color="blue"
                                    />
                                  ) : null;
                                })}
                            </MapContainer>
                            </div>

            
            
        </div>
    )   

}

export default GostMap