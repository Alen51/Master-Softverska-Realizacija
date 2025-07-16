using SoftverskaRealizacijaBackend.Models;

namespace SoftverskaRealizacijaBackend.Dto
{
    public class MapDataDto
    {
        public List<Node> Pins { get; set; }
        public List<NodeConnection> Lines { get; set; }
    }
}
