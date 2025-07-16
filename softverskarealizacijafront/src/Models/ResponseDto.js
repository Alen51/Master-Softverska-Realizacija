import ClientDto from "./ClientDto";

export default class ResponseDto{
    constructor(data){
        this.token = data.token;
        this.korisnikDto = new ClientDto(data.clientDto);
        this.result = data.result;
    }
}