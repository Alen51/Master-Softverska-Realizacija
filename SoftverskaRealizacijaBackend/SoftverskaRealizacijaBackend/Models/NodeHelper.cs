namespace SoftverskaRealizacijaBackend.Models
{
    public class NodeHelper
    {
        public Node Node { get; set; }
        public LineHelper ParentLine { get; set; }
        public List<LineHelper> OutgoingLines { get; set; } = new();
        public bool HasError { get; set; }
    }
}
