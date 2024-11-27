using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Library.Models.Emunerations;

namespace Library.Models
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
