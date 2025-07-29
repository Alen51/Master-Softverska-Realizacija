import React, { useState, useEffect } from "react";
import "leaflet/dist/leaflet.css";
import { getKvarList } from "../../Services/MapService";


const AdminErrors = () =>{

    const [errors, setErrors] = useState([]);

    const getErrors = async () => {

       
        setErrors([]);
        const data = await getKvarList();
        
        if(data !== null){
            setErrors(data);
            
            
            console.log("Errors:",errors);
        }
    }

    const fixProblem = (errorId) =>{
        
    }

    const handlePrikazDugmadi = (error) => {

        if(error.stanjeKvara == "Aktivan" ){
            return  <button className="ui red button" onClick={() => fixProblem(error.id)}>Fix Problem</button>
                       
        }
        else {
            return    null; 
        }
    }
    

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
                    Repported by Client 
                </td>
                <td>
                    Error reporting time
                </td>
                <td>
                    Status
                </td>

            </tr>
            {errors.map((error)=>(
                <tr>
                    <td>
                        {error.client}
                    </td>
                    <td>
                        {error.vremmePrijave}
                    </td>
                    <td>
                        {error.stanjeKvara}
                    </td>
                    <td>
                        {handlePrikazDugmadi(error)}
                    </td>
                </tr>
            ))}
            </table>



        </div>
        
    )

}

export default AdminErrors;