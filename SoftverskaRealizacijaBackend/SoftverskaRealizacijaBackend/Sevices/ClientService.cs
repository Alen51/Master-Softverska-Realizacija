using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Generators;
using SoftverskaRealizacijaBackend.Dto;
using SoftverskaRealizacijaBackend.Helpers;
using SoftverskaRealizacijaBackend.Infrastructure;
using SoftverskaRealizacijaBackend.Interfaces;
using SoftverskaRealizacijaBackend.Models;
using static SoftverskaRealizacijaBackend.Models.Enumerations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
            Client loginClient = new Client();
            if (string.IsNullOrEmpty(loginClientDto.Email) && string.IsNullOrEmpty(loginClientDto.Password))
            {
                return new ResponseDto("Niste uneli email ili lozinku.");
            }


            loginClient = await _dbContext.Clienti.FirstOrDefaultAsync(x => x.Email == loginClientDto.Email);

            if (loginClient == null)
                return new ResponseDto($"Client sa emailom {loginClientDto.Email} ne postoji");



            if (BCrypt.Net.BCrypt.Verify(loginClientDto.Password, loginClient.Password)) //(BCrypt.Net.BCrypt.Verify(loginClientDto.Lozinka, loginClient.Lozinka))
            {
                List<Claim> claims = new List<Claim>();
                if (loginClient.TipKorisnika == TipKorisnika.Administrator)
                    claims.Add(new Claim(ClaimTypes.Role, "administrator"));
                if (loginClient.TipKorisnika == TipKorisnika.Kupac)
                    claims.Add(new Claim(ClaimTypes.Role, "kupac"));
                if (loginClient.TipKorisnika == TipKorisnika.Gost)
                    claims.Add(new Claim(ClaimTypes.Role, "gost"));


                SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey.Value));
                SigningCredentials signInCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                JwtSecurityToken tokenOptions = new JwtSecurityToken(
                    issuer: "http://localhost:44385",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(40),
                    signingCredentials: signInCredentials
                    );

                string token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                ClientDto ClientDto = _mapper.Map<ClientDto>(loginClient);

                ResponseDto responseDto = new ResponseDto(token, ClientDto, "Uspesno ste se logovali na sistem");
                return responseDto;
            }
            else
            {
                return new ResponseDto("Lozinka nije ispravno uneta");
            }
        }

        public async Task<ResponseDto> Registration(ClientDto registerClient)
        {
            if (string.IsNullOrEmpty(registerClient.Email)) //ako nije unet email, baci gresku
                return new ResponseDto("Niste uneli email");

            foreach (Client k in _dbContext.Clienti)
            {
                if (k.Email == registerClient.Email)
                    return new ResponseDto("Email vec postoji");
            }

            

            


            if (!ClientHelper.IsClientFieldsValid(registerClient)) //ako nisu validna polja onda nista
                return new ResponseDto("Ostala polja moraju biti validna");

            ClientDto registeredClient = await AddClient(registerClient);

            if (registeredClient == null)
                return null;

            //nema provere za password, pa odmah vracamo token
            List<Claim> claims = new List<Claim>();
            if (registerClient.TipKorisnika == TipKorisnika.Administrator)
                claims.Add(new Claim(ClaimTypes.Role, "administrator"));
            if (registerClient.TipKorisnika == TipKorisnika.Kupac)
                claims.Add(new Claim(ClaimTypes.Role, "kupac"));
            if (registerClient.TipKorisnika == TipKorisnika.Gost)
                claims.Add(new Claim(ClaimTypes.Role, "gost"));

            SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey.Value));
            SigningCredentials signInCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken tokenOptions = new JwtSecurityToken(
                issuer: "http://localhost:44385",
                claims: claims,
                expires: DateTime.Now.AddMinutes(40),
                signingCredentials: signInCredentials
                );
            string token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            ResponseDto responseDto = new ResponseDto(token, registeredClient, "Uspesno ste se registrovali");
            return responseDto;
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

            updateClient.Password = ClientHelper.HashPassword(updateClientDto.Password
                );
            ClientHelper.UpdateClientFields(updateClient, updateClientDto);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<ClientDto>(updateClient);
        }
    }
}
