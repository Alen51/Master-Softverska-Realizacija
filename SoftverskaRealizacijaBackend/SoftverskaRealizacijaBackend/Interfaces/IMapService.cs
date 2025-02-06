using SoftverskaRealizacijaBackend.Dto;
using SoftverskaRealizacijaBackend.Models;

namespace SoftverskaRealizacijaBackend.Interfaces
{
    public interface IMapService
    {
        Task<MapDataDto> GetMapData();
        Task<Node> AddNode(Node nweNode);
        Task<NodeConnection> AddNodeConnection(NodeConnection newNodeConnection);
    }
}
