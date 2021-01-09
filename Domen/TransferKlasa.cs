using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domen
{
    public enum Operacije { Kraj = 1,
        Pogadjaj = 2
    }
    [Serializable]
    public class TransferKlasa
    {
        public Korisnik Korisnik { get; set; }
        public bool Ulogovan { get; set; }
        public string Poruka { get; set; }
        public bool Kraj { get; set; }
        public Operacije Operacija { get; set; }
        public int I { get; set; }
        public int Y { get; set; }
        public bool Pogodjen { get; set; }
    }
}
