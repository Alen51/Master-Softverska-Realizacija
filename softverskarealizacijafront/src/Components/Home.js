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

    const handleLine= (line,startPin,endPin)=>{
            if(line.hasError){
              return <Polyline
                            key={line.id}
                            positions={[
                            [startPin.latitude, startPin.longitude],
                            [endPin.latitude, endPin.longitude],
                            ]}
                            
                            color="red"
                        />
            }
            if(line.hasError==false){
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
    
        
    

    return (
        <div>
            <h1 color="red" className="ui blue center aligned header">

            Dobrodosli na stranicu

           </h1>
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