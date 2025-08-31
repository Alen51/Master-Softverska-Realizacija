export default class KvarDto{
    constructor(data){
        this.id=data.id;
        this.vremePrijave= data.vremePrijave;
        this.vremeOtkanjanja = data.vremeOtkanjanja;
        this.client=data.client;
        this.node=data.node;
        this.stanjeKvara=data.stanjeKvara;
    }

}