import React, { useState, useEffect } from "react";
import "leaflet/dist/leaflet.css";
import {MapContainer, TileLayer, Marker, Polyline,Popup, useMapEvents} from "react-leaflet" 
import { Icon, divIcon, point } from "leaflet";
import { getMapInfo, addNode, GetNodeConnections, GetNodes, addNodeConnection, addKvar, getKvarList } from "../Services/MapService";
import { useNavigate } from "react-router-dom";




const Home = () => {

    const [nodes,setNodes] = useState([]);
    const [loading, setLoading] = useState(true);
    const [lines, setLines] = useState([]);
    const [errors, setErrors] = useState([]);
    const [addingMode, setAddingMode] = useState(false);
    const [addingLineMode, setAddingLineMode] = useState(false);
    const [selectedPins, setSelectedPins] = useState([]);
      
    const customIcon = new Icon({
            // iconUrl: "https://cdn-icons-png.flaticon.com/512/447/447031.png",
            iconUrl: require("../Icons/placeholder.png"),
            iconSize: [38, 38] // size of the icon
          });
        
    const addPin = async (latlng) => {
            
      const nodeJSON=JSON.stringify({ latitude: latlng.lat, longitude: latlng.lng });
      console.log(nodeJSON);
      const newPin = addNode(nodeJSON);
      console.log("New node:" , newPin);
            //setNodes([...nodes, newPin]);
            
    }
        
    const addLine = async (startPin, endPin) => {

      const newLine = addNodeConnection(JSON.stringify({ startPinId: startPin.id, endPinId: endPin.id }))
      //setLines([...lines,newLine]);
      console.log("New line:", newLine);
        
    };

    

    const reportProblem = async (pinId) => {

        try {
          //logika o dodavanju kvara
          const kvarJSON=JSON.stringify({vremmePrijave: new Date().toISOString() , vremeOtklanjanja:null , client:1, node:pinId, stanjeKvara:"aktivan" })
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

    


    
    const handlePinSelect = (pin) => {
      if(addingLineMode){
        if (selectedPins.length === 0) {
          setSelectedPins([pin]);
        } else if (selectedPins.length === 1) {
          addLine(selectedPins[0], pin);
          setSelectedPins([]);
        }
      }
    };
        
    

    return (
        <div>
            <h1 color="red" className="ui blue center aligned header">

            Dobrodosli na stranicu

            </h1>
            <table className="ui fixed blue celled table">
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
                    {typeof( node.latitude)}
                  </td>
                </tr>
              ))}
              {lines.map((line)=>(
                <tr>
                  <td>
                    {line.startPinId}
                  </td>
                  <td>
                    {line.endPinId}
                  </td>
                  
                </tr>
              
              ))}
              {errors.map((e)=>(
                <tr>
                  <td>Error</td>
                  <td>
                    {e.id}
                  </td>
                  <td>
                    {e.node}
                  </td>
                  <td>
                    {e.stanjeKvara}
                  </td>
                  
                </tr>
              
              ))}
            </table>
            <div className="buttons-flex">
              <button className="ui blue button" onClick={() => setAddingMode(!addingMode)}>
                  {addingMode ? 'Disable Adding Pins' : 'Enable Adding Pins'}
              </button>
              <button className="ui blue button" onClick={() => getNodes()}>
                  Get Nodes
              </button>
              <button className="ui blue button" onClick={() => getLines()}>
                  Get Lines
              </button>
              <button className="ui blue button" onClick={() => getErrors()}>
                  Get Errors
              </button>
              <button className="ui blue button" onClick={() => setAddingMode(!addingMode)}>
                {addingMode ? 'Disable Adding Pins' : 'Enable Adding Pins'}
              </button>
              <button className="ui blue button" onClick={() => setAddingLineMode(!addingLineMode)}>
                {addingMode ? 'Disable Adding Lines' : 'Enable Adding Lines'}
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
                  <Marker key={node.id} position={[node.latitude, node.longitude]} icon={customIcon} 
                          eventHandlers={{
                              click: () => handlePinSelect(node), // Select pin on click
                            }}
                  >
                    
                    
            
                    <Popup>
                      <div>
                        <p><strong>Pin ID:</strong> {node.id}</p>
                        <button className="ui red button" onClick={() => reportProblem(node.id)}>Report Problem</button>
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