using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Library.Models.Emunerations;

namespace Library.Models
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
