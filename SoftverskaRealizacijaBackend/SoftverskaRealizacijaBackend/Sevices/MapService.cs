using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SoftverskaRealizacijaBackend.Dto;
using SoftverskaRealizacijaBackend.Infrastructure;
using SoftverskaRealizacijaBackend.Interfaces;
using SoftverskaRealizacijaBackend.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static SoftverskaRealizacijaBackend.Models.Enumerations;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public async Task<Kvar> AddKvar(Kvar newKvar)
        {
            if(_dbContext.Kvarovi.Contains(newKvar))
            {
                return null;
            }
            if (_dbContext.Kvarovi.Where(k => k.Node == newKvar.Node && k.StanjeKvara == Enumerations.StanjeKvara.Aktivan) != null)
            {
                _dbContext.Kvarovi.Add(newKvar);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                _dbContext.Kvarovi.Add(newKvar);
                await _dbContext.SaveChangesAsync();
                FindErrorOrigin();
            }
            return newKvar;
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

        public async Task<List<KvarDto>> GetKvarListDto()
        {
            List<Kvar> k = await _dbContext.Kvarovi.ToListAsync();
            return _mapper.Map<List<KvarDto>>(k);
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

        public async Task<List<LineHelper>> FindErrorOrigin()
        {
            List<Node> nodes = await _dbContext.Nodes.ToListAsync();
            List<Kvar> errors = await _dbContext.Kvarovi.ToListAsync();
            List<NodeConnection> connections = await _dbContext.NodeConnections.ToListAsync();

            var (nodeMap, lineMap) = BuildTree(nodes, connections, errors);

            var allNodes = nodeMap.Values.ToList();

            var rootNodes = allNodes.Where(n => n.ParentLine == null);

            foreach (var root in rootNodes)
            {
                PropagateErrors(root,allNodes);
            }

            foreach (var line in lineMap.Values)
            {
                var dbLine = _dbContext.NodeConnections.First(l => l.Id == line.Connection.Id);
                dbLine.HasError = line.HasError; 
            }
            _dbContext.SaveChangesAsync();


            return lineMap.Values.ToList();
        }

        public async Task<NodeConnectionDto> FixError(int lineId)
        {
            List<Node> nodes = await _dbContext.Nodes.ToListAsync();
            List<Kvar> errors = await _dbContext.Kvarovi.ToListAsync();
            List<NodeConnection> connections = await _dbContext.NodeConnections.ToListAsync();

            var (allNodes, lineMap) = BuildTree(nodes, connections, errors);

            if (!lineMap.TryGetValue(lineId, out var line))
                throw new Exception($"Line with ID {lineId} not found.");



            FixLineAndDescendants(lineId,lineMap,allNodes);

            _dbContext.SaveChanges();

            NodeConnectionDto outNode = new NodeConnectionDto();

            return _mapper.Map(_dbContext.NodeConnections.Find(lineId), outNode);
        }

        public (Dictionary<int, NodeHelper>, Dictionary<int, LineHelper> lineMap) BuildTree(
                                    List<Node> nodes,
                                    List<NodeConnection> connections,
                                    List<Kvar> errors)
        {
            var nodeMap = nodes.ToDictionary(n => n.Id, n => new NodeHelper
            {
                Node = n,
                HasError = errors.Any(e => e.Node == n.Id && e.StanjeKvara == Enumerations.StanjeKvara.Aktivan)
            });

            var lineMap = new Dictionary<int, LineHelper>();

            foreach (var conn in connections)
            {
                var lineHelper = new LineHelper { Connection = conn };
                lineMap[conn.Id] = lineHelper;

                var parentNode = nodeMap[conn.StartPinId];
                var childNode = nodeMap[conn.EndPinId];

                parentNode.OutgoingLines.Add(lineHelper);
                childNode.ParentLine = lineHelper;
            }

            return (nodeMap, lineMap);
        }

        void PropagateErrors(NodeHelper node, List<NodeHelper> allNodes)
        {
            if (node.HasError && node.ParentLine != null)
            {
                node.ParentLine.HasError = true;
            }

            // Step 2: Recurse into children first
            foreach (var line in node.OutgoingLines)
            {
                var childNode = allNodes.First(nw => nw.Node.Id == line.Connection.EndPinId);
                PropagateErrors(childNode, allNodes);
            }

            // Step 3: If all child lines have error → mark parent line
            if (node.OutgoingLines.Count > 0 &&
                node.OutgoingLines.All(l => l.HasError))
            {
                if (node.ParentLine != null)
                {
                    node.ParentLine.HasError = true;
                    foreach (var line in node.OutgoingLines)
                    {
                        line.HasError = false;
                    }
                }
            }
        }

        public void FixLineAndDescendants(int lineId, Dictionary<int, LineHelper> lineMap,
                                                      Dictionary<int, NodeHelper> nodeMap)
        {
            if (!lineMap.TryGetValue(lineId, out var line))
                return;

            // Fix this line
            line.HasError = false;
            _dbContext.Update(line.Connection);

            var node = nodeMap[line.Connection.EndPinId];

            var endNode = nodeMap[line.Connection.EndPinId];
            var activeErrors = _dbContext.Kvarovi
                .Where(e => e.Node == endNode.Node.Id && e.StanjeKvara == StanjeKvara.Aktivan)
                .ToList();

            foreach (var err in activeErrors)
            {
                err.StanjeKvara = StanjeKvara.Popravljen;
                err.VremeOtkanjanja = DateTime.UtcNow;
                _dbContext.Update(err);
            }

            // 3. Cascade downwards only if ALL outgoing lines from this node
            // lead to end-nodes that *still* have active errors
            if (endNode.OutgoingLines.Count > 0 &&
                endNode.OutgoingLines.All(l =>
                    _dbContext.Kvarovi.Any(e =>
                        e.Node == l.Connection.EndPinId && e.StanjeKvara == StanjeKvara.Aktivan)))
            {
                foreach (var childLine in endNode.OutgoingLines)
                {
                    FixLineAndDescendants(childLine.Connection.Id, lineMap, nodeMap);
                }
            }
        }
    }
}
