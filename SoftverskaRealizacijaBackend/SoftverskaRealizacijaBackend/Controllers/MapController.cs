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

        [HttpGet("getMapData")]
        public async Task<IActionResult> GetMapData()
        {
            MapDataDto m = await _mapService.GetMapDataDto();
            return Ok(m);
        }

        [HttpGet("getAllNodes")]
        public async Task<IActionResult> GetAllNodes()
        {
            List<NodeDto> nodes = await _mapService.GetAllNodes();
            return Ok(nodes);
        }

        [HttpGet("getAllNodeConnections")]
        public async Task<IActionResult> GetAllNodeConnections()
        {
            List<NodeConnectionDto> nodeConnections = await _mapService.GetAllNodeConnections();
            return Ok(nodeConnections);
        }

        [HttpGet("getKvarList")]
        public async Task<IActionResult> GetKvarList()
        {
            List<KvarDto> kvarList = await _mapService.GetKvarListDto();
            return Ok(kvarList);
        }

        [HttpGet("getErrorOrigin")]
        public async Task<IActionResult> GetErrorOrigin()
        {
            List<LineHelper> lh = await _mapService.FindErrorOrigin();
            return Ok(lh);
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

        [HttpPost("addKvar")]
        public async Task<IActionResult> AddKvar([FromBody] Kvar kvar)
        {
            return Ok(await _mapService.AddKvar(kvar));
        }

        [HttpPost("fixError")]
        public async Task<IActionResult> FixError([FromBody] int lineId)
        {
            return Ok(await _mapService.FixError(lineId));
        }
    }
}
