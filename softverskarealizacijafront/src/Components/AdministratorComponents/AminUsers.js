import React, { useState, useEffect } from "react";
import "leaflet/dist/leaflet.css";
import { GetAllUsers } from "../../Services/ClientService";

const AdminUsers = () =>{

    const [users, setUsers] = useState([]);

    const getUsers = async () => {

       
        setUsers([]);
        const data = await GetAllUsers();
        
        if(data !== null){
            setUsers(data);
            
            
            console.log("Users:",users);
        }
    }

    

    
    

    return (
        <div>
            <h1 color="red" className="ui blue center aligned header">

            Users list

            </h1>
            <button className="ui blue button" onClick={() => getUsers()}>
                                Get Users
            </button>

            <table className="ui fixed blue celled table">
            <tr>
                <td>
                    User ID
                </td>
                <td>
                    Full Name
                </td>
                <td>
                    Email
                </td>
            
                <td>
                    Tip Korisnika
                </td>

            </tr>
            {users.map((user)=>(
                <tr>
                    <td>
                        {user.id}
                    </td>
                    <td>
                        {user.fullName}
                    </td>
                    <td>
                        {user.email}
                    </td>
                    <td>
                        {user.tipKorisnika}
                    </td>
                </tr>
            ))}
            </table>



        </div>
        
    )

}

export default AdminUsers;