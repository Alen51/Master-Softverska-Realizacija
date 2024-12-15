using SoftverskaRealizacijaBackend.Dto;

namespace SoftverskaRealizacijaBackend.Interfaces
{
    public interface IClientService
    {

        Task<ClientDto> AddClient(ClientDto newClientDto);
        Task<List<ClientDto>> GetAllClients();
        Task<ClientDto> GetClientById(long id);
        Task<ClientDto> UpdateClient(long id, ClientDto updateClientDto);
        Task DeleteClient(long id);

        Task<ResponseDto> Login(LoginDto loginKorisnikDto);
        Task<ResponseDto> Registration(ClientDto registerKorisnik);
    }
}
