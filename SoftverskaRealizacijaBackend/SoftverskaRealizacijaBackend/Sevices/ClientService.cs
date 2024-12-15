using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SoftverskaRealizacijaBackend.Dto;
using SoftverskaRealizacijaBackend.Helpers;
using SoftverskaRealizacijaBackend.Infrastructure;
using SoftverskaRealizacijaBackend.Interfaces;
using SoftverskaRealizacijaBackend.Models;

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
        public async Task<ClientDto> AddClient(ClientDto newClientDto)
        {
            Client newClient = _mapper.Map<Client>(newClientDto);
            newClient.Password= ClientHelper.HashPassword(newClientDto.Password);
            _dbContext.Clienti.Add(newClient);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<ClientDto>(newClient);
        }

        public async Task DeleteClient(long id)
        {
            Client deleteClient = _dbContext.Clienti.Find(id);
            _dbContext.Clienti.Remove(deleteClient);
            await _dbContext.SaveChangesAsync();


        }

        public async Task<List<ClientDto>> GetAllClients()
        {
            List<Client> korisnici = await _dbContext.Clienti.ToListAsync();
            return _mapper.Map<List<ClientDto>>(korisnici);
        }

        public async Task<ClientDto> GetClientById(long id)
        {
            Client findClient = await _dbContext.Clienti.FindAsync(id);
            return _mapper.Map<ClientDto>(findClient);
        }

        public async Task<ResponseDto> Login(LoginDto loginClientDto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto> Registration(ClientDto registerClient)
        {
            throw new NotImplementedException();
        }

        public async Task<ClientDto> UpdateClient(long id, ClientDto updateClientDto)
        {
            Client updateClient = await _dbContext.Clienti.FindAsync(id);

            if (updateClient == null)
            {
                return null;
            }
            /*
            if (!ClientHelper.IsClientFieldsValid(updateClientDto))
                return null;*/

            updateClient.Password = ClientHelper.HashPassword(updateClientDto.Password);
            ClientHelper.UpdateClientFields(updateClient, updateClientDto);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<ClientDto>(updateClient);
        }
    }
}
