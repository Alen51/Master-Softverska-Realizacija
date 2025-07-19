import React, { useState, useEffect } from "react";
import "leaflet/dist/leaflet.css";
import { getKvarList } from "../../Services/MapService";


const AdminErrors = () =>{

    const [errors, setErrors] = useState([]);

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

            Lista prijavljenih kvarova

            </h1>
            <button className="ui blue button" onClick={() => getErrors()}>
                                Get Errors
            </button>

            <table>
            <tr>
                <td>
                    Repported by Client 
                </td>
                <td>
                    Vreme prijave
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
                </tr>
            ))}
            </table>



        </div>
        
    )

}

export default AdminErrors;