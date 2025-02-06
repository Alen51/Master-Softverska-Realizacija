import axios from "../api/axios";

export const getMapInfo = async()=>{
    const GET_MAP_URI="/map/getAll"

    try{
        const { data } = await axios.get(
            `${process.env.REACT_APP_API_BACK}${GET_MAP_URI}`,
                {
                    headers: {
                        'Content-Type': 'application/json',
                        
                    },
                    withCredentials: true
                }
            );
            console.log(data)
        return data;
    }catch(err){
        alert("Nesto se desilo prilikom dobavljanja informacija o mapi");
        return null;
    }
}

export const addNode = async(node)=>{
    const ADD_PIN_URI="/map/addNode"

    try{
        const {data}=await axios.post(
            `${process.env.REACT_APP_API_BACK}${ADD_PIN_URI}`,
            node,
            {
                headers: {
                "Content-Type": "application/json",
                
                },
                withCredentials:true 
            }
        );
        return data;

    }catch(err){
        alert("Nesto se desilo prilikom dodavanja Noda na mapu");
        return null;
    }
}

export const addNodeConnection = async(nodeConnection)=>{
    const ADD_PIN_URI="/map/addNodeConnection"

    try{
        const {data}=await axios.post(
            `${process.env.REACT_APP_API_BACK}${ADD_PIN_URI}`,
            nodeConnection,
            {
                headers: {
                "Content-Type": "application/json",
                
                },
                withCredentials:true
            }
        );
        return data;

    }catch(err){
        alert("Nesto se desilo prilikom dodavanja Line-a na mapu");
        return null;
    }
}