import React, { useState, useEffect } from "react";
import "leaflet/dist/leaflet.css";
import { GetNodeConnections, fixError } from "../../Services/MapService";


const AdminErrorFix = () =>{

    const [lines, setLines] = useState([]);

    const getLines = async () => {

       
        setLines([]);
        const data = await GetNodeConnections();
        
        if(data !== null){
            setLines(data);
            
            
            console.log("Lines:",lines);
        }
    }

    const fixErrorLine = async (lineId) =>{

        const data = await fixError(lineId);
        getLines();

    }

    const handleButton = (line) => {

        if( line.hasError ){
            return  <button className="ui red button" onClick={() => fixErrorLine(line.id)}>Fix Problem</button>
                       
        }
        else {
            return    null; 
        }
    }
    

    return (
        <div>
            <h1 color="red" className="ui blue center aligned header">

            Error lines

            </h1>
            <button className="ui blue button" onClick={() => getLines()}>
                                Get Lines
            </button>

            <table className="ui fixed blue celled table">
            <tr>
                
                <td>
                    Line Id
                </td>

            </tr>
            {lines.map((line)=>(
                <tr>
                    
                    <td>
                        {line.id}
                    </td>
                    <td>
                        {handleButton()}
                    </td>
                </tr>
            ))}
            </table>



        </div>
        
    )

}

export default AdminErrorFix;