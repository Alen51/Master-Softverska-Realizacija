namespace SoftverskaRealizacijaBackend.Models
{
    public class Node
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public List<Kvar> Kvarovi { get; set; }
    }
}
