import React, { useState, useEffect } from "react";
import "leaflet/dist/leaflet.css";
import {MapContainer, TileLayer, Marker, Polyline,Popup, useMapEvents} from "react-leaflet" 
import { Icon, divIcon, point } from "leaflet";
import { getMapInfo, addNode, addNodeConnection } from "../../Services/MapService";
import { useNavigate } from "react-router-dom";


const AdminMap = () =>{

    const [pins, setPins] = useState([]);
      const [lines, setLines] = useState([]);
      const [addingMode, setAddingMode] = useState(false);
      const [selectedPins, setSelectedPins] = useState([]);
    
      const navigate = useNavigate();
      //const token = sessionStorage.getItem('token')
    
    
      const customIcon = new Icon({
        // iconUrl: "https://cdn-icons-png.flaticon.com/512/447/447031.png",
        iconUrl: require("../../Icons/placeholder.png"),
        iconSize: [38, 38] // size of the icon
      });
    
      useEffect(() => {
        const fetchData = async () => {
          const data = getMapInfo();
          setPins(data.pins || []);
          setLines(data.lines || []);
        };
    
        fetchData();
      }, []);
    
              
    
      const addPin = async (latlng) => {
        const newPin = addNode(JSON.stringify({ latitude: latlng.lat, longitude: latlng.lng }));
        setPins([...pins, newPin]);
      }
    
      const addLine = async (startPin, endPin) => {
        const newLine = addNodeConnection(JSON.stringify({ startPinId: startPin.id, endPinId: endPin.id }))
        setLines([...lines,newLine]);
          
      };
      
    
      const handlePinSelect = (pin) => {
        if (selectedPins.length === 0) {
          setSelectedPins([pin]);
        } else if (selectedPins.length === 1) {
          addLine(selectedPins[0], pin);
          setSelectedPins([]);
        }
      };
    
      const reportProblem = async (pinId) => {
        try {
          await fetch(`/api/map-data/report-problem/${pinId}`, { method: 'POST' });
          alert(`Reported problem for pin ID: ${pinId}`);
        } catch (error) {
          console.error('Failed to report problem:', error);
        }
      };
    
    
        return(
            <>
                <h1 className="ui blue center aligned icon header">
                    <i className="briefcase icon"></i>
                    Dobro dosli na stranicu 
                    <div className="sub header">
                        Pre nego sto nastavite dalje, molimo Vas ulogujte se na Vas nalog <br/>
                        Ako vec niste logovani, molimo Vas da se registrujete
                    </div>
                </h1>
               
                <div className="buttons-flex">
                <button className="ui blue button" onClick={() => setAddingMode(!addingMode)}>
                    {addingMode ? 'Disable Adding Pins' : 'Enable Adding Pins'}
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
                    {pins.map((pin, index) => (
                      <Marker>
                        key={index}
                        position={pin}
                        icon={customIcon}
                        
                        eventHandlers={{
                          click: () => handlePinSelect(pin), // Select pin on click
                        }}
                
                        <Popup>
                          <div>
                            <p><strong>Pin ID:</strong> {pin.id}</p>
                            <button onClick={() => reportProblem(pin.id)}>Report Problem</button>
                          </div>
                        </Popup>
                        </Marker>
                    ))}
    
                    {lines.map((line) => {
                      const startPin = pins.find(p => p.id === line.startPinId);
                      const endPin = pins.find(p => p.id === line.endPinId);
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
            </>
        );
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

export default  AdminMap;

