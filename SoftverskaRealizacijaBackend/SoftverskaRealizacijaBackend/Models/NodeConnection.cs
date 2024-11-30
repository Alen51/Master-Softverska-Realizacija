namespace SoftverskaRealizacijaBackend.Models
{
    public class NodeConnection
    {
        public int Id { get; set; }
        public Node TopNode { get; set; }
        public List<Node> Nodes { get; set; }
    }
}
