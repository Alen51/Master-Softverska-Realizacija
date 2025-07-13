using AutoMapper;
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
        private readonly IMapper _mapper;

        public MapService(IMapper mapper, CRUDContext dbContext)
        {
            _mapper = mapper;
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

        public async Task<List<NodeConnectionDto>> GetAllNodeConnections()
        {
            List<NodeConnection> nc= await _dbContext.NodeConnections.ToListAsync();
            return _mapper.Map<List<NodeConnectionDto>>(nc);
        }

        public async Task<List<NodeDto>> GetAllNodes()
        {
            List<Node> n = await _dbContext.Nodes.ToListAsync();
            return _mapper.Map<List<NodeDto>>(n);
        }

        public async Task<MapDataDto> GetMapDataDto()
        {
             MapDataDto map = new MapDataDto();
            map.Lines = new List<NodeConnection>();
            map.Pins = new List<Node>();

            map.Pins= await _dbContext.Nodes.ToListAsync();
            map.Lines = await _dbContext.NodeConnections.ToListAsync();

            return map;

        }
    }
}
