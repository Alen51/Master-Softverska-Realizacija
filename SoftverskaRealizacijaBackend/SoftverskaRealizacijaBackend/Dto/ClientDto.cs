using static SoftverskaRealizacijaBackend.Models.Enumerations;

namespace SoftverskaRealizacijaBackend.Dto
{
    public class ClientDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public TipKorisnika TipKorisnika { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
