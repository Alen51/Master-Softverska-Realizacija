import React, { useState, useEffect } from "react";
import "leaflet/dist/leaflet.css";
import {MapContainer, TileLayer, Marker, Polyline,Popup, useMapEvents} from "react-leaflet" 
import { Icon, divIcon, point } from "leaflet";
import { getMapInfo, addNode, addNodeConnection, GetNodes } from "../Services/MapService";
import { useNavigate } from "react-router-dom";
import { GetAllUsers } from "../Services/ClientService";
import axios from "axios";


const Home = () => {

    const [nodes,setNodes] = useState([]);
    const [loading, setLoading] = useState(true);
    const [lines, setLines] = useState([]);
    const [addingMode, setAddingMode] = useState(false);
    const [selectedPins, setSelectedPins] = useState([]);
      
    const customIcon = new Icon({
            // iconUrl: "https://cdn-icons-png.flaticon.com/512/447/447031.png",
            iconUrl: require("../Icons/placeholder.png"),
            iconSize: [38, 38] // size of the icon
          });
        
    const addPin = async(latlng)=>{}
    const addLine = async(startPin,endPin)=>{}
    const reportProblem = async (pinId) => {}
    const newMarker = [45.2396,19.8227]
    const getNodes = async () => {
            setLoading(true);

            const data = await GetNodes();
            
            if(data !== null){
                setNodes(data);
                
                setLoading(false);
                console.log("Nodes:",nodes);
            }
    }


    
    const handlePinSelect = (pin) => {
        if (selectedPins.length === 0) {
          setSelectedPins([pin]);
        } else if (selectedPins.length === 1) {
          addLine(selectedPins[0], pin);
          setSelectedPins([]);
        }
      };
        
    

    return (
        <div>
            <h1 color="red" className="ui blue center aligned header">

            Dobrodosli na stranicu

            </h1>
            <table>
              <tr>
                <td>
                  Id
                </td>
                <td>
                  lat
                </td>
                <td>
                  long
                </td>
              </tr>
              {nodes.map((node)=>(
                <tr>
                  <td>
                    {node.id}
                  </td>
                  <td>
                    {node.latitude}
                  </td>
                  <td>
                    {node.longitude}
                  </td>
                  <td>
                    { node.latitude}
                  </td>
                </tr>
              ))}
            </table>
            <div className="buttons-flex">
                            <button className="ui blue button" onClick={() => setAddingMode(!addingMode)}>
                                {addingMode ? 'Disable Adding Pins' : 'Enable Adding Pins'}
                            </button>
                            <button className="ui blue button" onClick={() => getNodes()}>
                                Get Map Data
                            </button>
                            
                            
                            </div>
                            <div></div>
                            
                
                            <div className="test">
                            <MapContainer center={[45.2396,19.8227]} zoom={15}>
                                <TileLayer
                                    attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
                                    url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                                />
                                <MapClickHandler addingMode={addingMode} addPin={addPin} />
                                
                                {nodes.map((node)=>(
                                  <Marker  position={[node.latitude, node.longitude]} icon={customIcon} >
                                    
                                    
                                    
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

const MapClickHandler = ({ addingMode, addPin }) => {
    useMapEvents({
      click: (e) => {
        if (addingMode) {
          addPin(e.latlng); // Add pin if adding mode is active
         
        }
      },
    });
    return null;
  };
  


export default Home;