using SoftverskaRealizacijaBackend.Dto;
using SoftverskaRealizacijaBackend.Models;

namespace SoftverskaRealizacijaBackend.Interfaces
{
    public interface IMapService
    {
        Task<List<NodeDto>> GetAllNodes();
        Task<List<NodeConnectionDto>> GetAllNodeConnections();
        Task<Node> AddNode(Node nweNode);
        Task<NodeConnection> AddNodeConnection(NodeConnection newNodeConnection);
        Task<MapDataDto> GetMapDataDto();
        Task<Kvar> AddKvar(Kvar newKvar);
        Task<List<KvarDto>> GetKvarListDto();
    }
}
