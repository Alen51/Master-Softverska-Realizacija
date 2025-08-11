namespace SoftverskaRealizacijaBackend.Dto
{
    public class NodeConnectionDto
    {
        public int Id { get; set; }
        public int StartPinId { get; set; }
        public int EndPinId { get; set; }
        public bool HasError { get; set; }
    }
}
