import axios from "../api/axios";
import NodeConnectionDto from "../Models/NodeConnectionDto";
import NodeDto from "../Models/NodeDto";
import KvarDto from "../Models/KvarDto";

export const GetNodes = async()=>{
    const GET_MAP_URI="/map/getAllNodes"

    try{
        const { data } = await axios.get(
            `${process.env.REACT_APP_API_BACK}${GET_MAP_URI}`,
                {
                    headers: {
                        'Content-Type' : 'application/json',
                        
                    },
                    
                }
            );
            console.log(data)
            const nodes=data.map(node=>{
                return new NodeDto(node);
            })
        return nodes;
    }catch(err){
        alert("Nesto se desilo prilikom dobavljanja informacija o nodovima");
        return null;
    }
}

export const GetNodeConnections = async()=>{
    const GET_MAP_URI="/map/getAllNodeConnections"

    try{
        const { data } = await axios.get(
            `${process.env.REACT_APP_API_BACK}${GET_MAP_URI}`,
                {
                    headers: {
                        'Content-Type' : 'application/json',
                        
                    },
                   
                }
            );
            console.log(data)
        const lines=data.map(line=>{
            return new NodeConnectionDto(line);
        })    
        return lines;
    }catch(err){
        alert("Nesto se desilo prilikom dobavljanja informacija o linijama");
        return null;
    }
}

export const getKvarList = async()=>{
    const GET_KVAR_URI="/map/getKvarList"

    try{
        const { data } = await axios.get(
            `${process.env.REACT_APP_API_BACK}${GET_KVAR_URI}`,
                {
                    headers: {
                        'Content-Type' : 'application/json',
                        
                    },
                   
                }
            );
            console.log(data)
        const kvarovi=data.map(kvar=>{
            return new KvarDto(kvar);
        })    
        return kvarovi;
    }catch(err){
        alert("Nesto se desilo prilikom dobavljanja informacija o kvarovima");
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
        console.log(data);
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

export const addKvar = async(newKvar)=>{
const ADD_KVAR_URI="/map/addKvar"

    try{
        const {data}=await axios.post(
            `${process.env.REACT_APP_API_BACK}${ADD_KVAR_URI}`,
            newKvar,
            {
                headers: {
                "Content-Type": "application/json",
                
                },
                withCredentials:true
            }
        );
        return data;

    }catch(err){
        alert("Nesto se desilo prilikom dodavanja Kvara na mapu");
        return null;
    }
}

