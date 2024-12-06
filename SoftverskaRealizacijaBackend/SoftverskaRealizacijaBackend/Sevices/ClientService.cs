using AutoMapper;
using SoftverskaRealizacijaBackend.Dto;
using SoftverskaRealizacijaBackend.Infrastructure;
using SoftverskaRealizacijaBackend.Interfaces;

namespace SoftverskaRealizacijaBackend.Sevices
{
    public class ClientService : IClientService
    {
        private readonly IMapper _mapper;
        private readonly CRUDContext _dbContext;
        private readonly IConfigurationSection _secretKey;

        public ClientService(IMapper mapper, CRUDContext dbContext, IConfiguration configuration)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _secretKey = configuration.GetSection("SecretKey");
        }
        public Task<ClientDto> AddClient(ClientDto newClientDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteKorisnik(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ClientDto>> GetAllClients()
        {
            throw new NotImplementedException();
        }

        public Task<ClientDto> GetKorisnikById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto> Login(LoginDto loginKorisnikDto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto> Registration(ClientDto registerKorisnik)
        {
            throw new NotImplementedException();
        }

        public Task<ClientDto> UpdateKorisnik(long id, ClientDto updateClientDto)
        {
            throw new NotImplementedException();
        }
    }
}
