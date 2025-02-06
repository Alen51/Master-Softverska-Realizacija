using Microsoft.EntityFrameworkCore;
using SoftverskaRealizacijaBackend.Dto;
using SoftverskaRealizacijaBackend.Infrastructure;
using SoftverskaRealizacijaBackend.Interfaces;
using SoftverskaRealizacijaBackend.Models;

namespace SoftverskaRealizacijaBackend.Sevices
{
    public class MapService : IMapService
    {

        private readonly CRUDContext _dbContext;

        public MapService(CRUDContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Node> AddNode(Node newNode)
        {
            if(_dbContext.Nodes.Contains(newNode))
            {
                return null;
            }

            _dbContext.Nodes.Add(newNode);
            await _dbContext.SaveChangesAsync();

            return newNode;
            
        }

        public async Task<NodeConnection> AddNodeConnection(NodeConnection newNodeConnection)
        {
            if (_dbContext.NodeConnections.Contains(newNodeConnection))
            {
                return null;
            }

            _dbContext.NodeConnections.Add(newNodeConnection); ;
            await _dbContext.SaveChangesAsync();

            return newNodeConnection;
           
        }

        public async Task<MapDataDto> GetMapData()
        {
            MapDataDto mapDataDto = new MapDataDto();
            mapDataDto.Lines=new List<NodeConnection>();
            mapDataDto.Pins=new List<Node>();

            mapDataDto.Lines = await _dbContext.NodeConnections.ToListAsync();
            mapDataDto.Pins = await _dbContext.Nodes.ToListAsync();


            return mapDataDto;
        }
    }
}
