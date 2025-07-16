export default class KvarDto{
    constructor(data){
        this.id=data.id;
        this.vremmePrijave= data.vremmePrijave;
        this.vremeOtklanjanja = data.vremeOtklanjanja;
        this.client=data.client;
        this.node=data.node;
        this.stanjeKvara=data.stanjeKvara;
    }

}