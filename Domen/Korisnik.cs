using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domen
{
    [Serializable]
    public class Korisnik
    {
        public string Mail { get; set; }
        public string Password { get; set; }
        public int Dimenzija { get; set; }
        public string[,] Matrica { get; set; }
        public int MaxBrPokusaja { get; set; }
        public int BrojPokusaka { get; set; }
        public int BrojPogodata { get; set; }

        public override bool Equals(object obj)
        {
            var x = obj as Korisnik;
            return (x != null && x.Mail == this.Mail && x.Password == this.Password);
        }
    }
}
