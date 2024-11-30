using static SoftverskaRealizacijaBackend.Models.Enumerations;

namespace SoftverskaRealizacijaBackend.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TipKorisnika TipKorisnika { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
