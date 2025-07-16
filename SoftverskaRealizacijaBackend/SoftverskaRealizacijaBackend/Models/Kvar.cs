using static SoftverskaRealizacijaBackend.Models.Enumerations;

namespace SoftverskaRealizacijaBackend.Models
{
    public class Kvar
    {
        
        public int Id { get; set; }
        public DateTime VremePrijave { get; set; }
        public DateTime VremeOtkanjanja { get; set; }
        public int Client { get; set; }
        public int Node { get; set; }
        public StanjeKvara StanjeKvara { get; set; }
    }
}
