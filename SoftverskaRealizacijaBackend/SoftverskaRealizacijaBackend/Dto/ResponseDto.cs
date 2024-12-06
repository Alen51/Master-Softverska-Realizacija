namespace SoftverskaRealizacijaBackend.Dto
{
    public class ResponseDto
    {
        public string Token { get; set; }
        public ClientDto ClientDto { get; set; }
        public string Result { get; set; }

        public ResponseDto()
        {
            Token = "";
            ClientDto = null;
            Result = "";
        }

        public ResponseDto(string result)
        {
            Token = "";
            ClientDto = null;
            Result = result;
        }

        public ResponseDto(string token, ClientDto clientDto, string result)
        {
            Token = token;
            ClientDto = clientDto;
            Result = result;
        }
    }
}
