using Microsoft.AspNetCore.Mvc;
using SoftverskaRealizacijaBackend.Dto;
using SoftverskaRealizacijaBackend.Interfaces;
using SoftverskaRealizacijaBackend.Models;
using SoftverskaRealizacijaBackend.Sevices;

namespace SoftverskaRealizacijaBackend.Controllers
{
    [Route("api/map")]
    [ApiController]
    public class MapController : ControllerBase
    {
        private readonly IMapService _mapService;

        public MapController(IMapService mapService) { 
            _mapService = mapService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            MapDataDto mapa = await _mapService.GetMapData();
            return Ok(mapa);
        }

        [HttpPost("addNode")]
        public async Task<IActionResult> AddNode([FromBody] Node node)
        {
            return Ok(await _mapService.AddNode(node));
        }

        [HttpPost("addNodeConnection")]
        public async Task<IActionResult> AddNodeConnnection([FromBody] NodeConnection nodeConnection)
        {
            return Ok(await _mapService.AddNodeConnection(nodeConnection));
        }
    }
}
