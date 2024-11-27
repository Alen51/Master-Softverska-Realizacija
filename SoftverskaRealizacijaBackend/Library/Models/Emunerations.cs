using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Emunerations
    {
        public enum TipKorisnika
        {
            Administrator,
            Gost,
            Kupac
        }

        public enum StanjeKvara
        {
            Aktivan,
            Popravljen
        }
    }
}
