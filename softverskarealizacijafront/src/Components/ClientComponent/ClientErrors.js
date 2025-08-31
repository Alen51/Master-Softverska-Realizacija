import React, { useState, useEffect } from "react";
import "leaflet/dist/leaflet.css";
import { getKvarList } from "../../Services/MapService";


const ClientErrors = () =>{

    const [errors, setErrors] = useState([]);

    const getErrors = async () => {

        
        setErrors([]);
        const data = await getKvarList();
        
        if(data !== null){
            setErrors(data);
            
            
            console.log("Errors:",errors);
        }
    }

    const formatDate = (dateString) => {
       console.log("Incoming date raw:", dateString);

        // if empty or default .NET "0001" value
        if (!dateString || dateString.startsWith("0001")) {
            return "—";
        }

        const date = new Date(dateString);
        if (isNaN(date.getTime())) {
            console.warn("Invalid parsed date:", dateString);
            return "—";
        }

        return date.toLocaleString("sr-RS", {
            year: "numeric",
            month: "2-digit",
            day: "2-digit",
            hour: "2-digit",
            minute: "2-digit",
            second: "2-digit"
        });
    };





    return (
        <div>
            <h1 color="red" className="ui blue center aligned header">

            Reported errors list

            </h1>
            <button className="ui blue button" onClick={() => getErrors()}>
                                Get Errors
            </button>

            <table className="ui fixed blue celled table">
            <tr>
                <td>
                    Error reporting time
                </td>
                <td>
                    Error fix time
                </td>
                <td>
                    Status
                </td>

            </tr>
            {errors.map((error)=>(
                <tr>
                    <td>{formatDate(error.vremePrijave)}</td>
                    <td>{formatDate(error.
                    vremeOtkanjanja
                    )}</td>
                    <td>
                        {error.stanjeKvara}
                    </td>
                </tr>
            ))}
            

            </table>




        </div>
        
    )

}

export default ClientErrors;