using static SoftverskaRealizacijaBackend.Models.Enumerations;

namespace SoftverskaRealizacijaBackend.Models
{
    public class Kvar
    {
        
        public int Id { get; set; }
        public DateTime VremePrijave { get; set; }
        public DateTime VremeOtkanjanja { get; set; }
        public Client Client { get; set; }
        public Node Node { get; set; }
        public StanjeKvara StanjeKvara { get; set; }
    }
}
