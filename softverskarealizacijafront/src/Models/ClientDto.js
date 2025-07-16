export default class ClientDto{
    constructor(data){
        this.id = data.id;
        this.fullName = data.fullName;
        this.email = data.email;
        this.password = data.password;
        this.ime = data.ime;
        
        this.tipKorisnika = data.tipKorisnika;
           
    }
}