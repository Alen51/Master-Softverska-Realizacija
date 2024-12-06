using SoftverskaRealizacijaBackend.Dto;

namespace SoftverskaRealizacijaBackend.Interfaces
{
    public interface IClientService
    {

        Task<ClientDto> AddClient(ClientDto newClientDto);
        Task<List<ClientDto>> GetAllClients();
        Task<ClientDto> GetKorisnikById(long id);
        Task<ClientDto> UpdateKorisnik(long id, ClientDto updateClientDto);
        Task DeleteKorisnik(long id);

        Task<ResponseDto> Login(LoginDto loginKorisnikDto);
        Task<ResponseDto> Registration(ClientDto registerKorisnik);
    }
}
