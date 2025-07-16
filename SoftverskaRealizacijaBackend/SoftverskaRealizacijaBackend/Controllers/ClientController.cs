using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoftverskaRealizacijaBackend.Dto;
using SoftverskaRealizacijaBackend.Interfaces;

namespace SoftverskaRealizacijaBackend.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            List<ClientDto> korisnici = await _clientService.GetAllClients();
            return Ok(korisnici);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientById(long id)
        {
            return Ok(await _clientService.GetClientById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] ClientDto Client)
        {
            return Ok(await _clientService.AddClient(Client));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ChangeClient(long id, [FromBody] ClientDto Client)
        {
            ClientDto updatedClient = await _clientService.UpdateClient(id, Client);
            if (updatedClient == null)
            {
                return BadRequest("Postoje neka prazna polja(mozda Client ne postoji)");
            }

            updatedClient.Password = Client.Password;
            return Ok(updatedClient);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(long id)
        {
            await _clientService.DeleteClient(id);
            return Ok($"Client sa id = {id} je uspesno obrisan.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginClientDto)
        {
            ResponseDto responseDto = await _clientService.Login(loginClientDto);
            if (responseDto.ClientDto == null)
            {
                return BadRequest(responseDto.Result);
            }

            responseDto.ClientDto.Password = loginClientDto.Password;
            return Ok(responseDto);
        }

        [HttpPost("registration")]
        public async Task<IActionResult> Registration([FromBody] ClientDto registerClientDto)
        {
            ResponseDto responseDto = await _clientService.Registration(registerClientDto);
            if (responseDto.ClientDto == null)
                return BadRequest(responseDto.Result);

            responseDto.ClientDto.Password = registerClientDto.Password;
            return Ok(responseDto);
        }
    }
}
