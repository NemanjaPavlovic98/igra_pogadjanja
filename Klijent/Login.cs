using Domen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Klijent
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Komunikacija k = new Komunikacija();
            TransferKlasa transfer = new TransferKlasa();
            Korisnik korisnik = new Korisnik();

            korisnik.Mail = txtMail.Text;
            korisnik.Password = txtPass.Text;
          
                switch (comboBox1.SelectedItem.ToString())
                {
                    case "3X3":
                        korisnik.Dimenzija = 3;
                        break;
                    case "4X4":
                        korisnik.Dimenzija = 4;
                        break;
                    case "5X5":
                        korisnik.Dimenzija = 5;
                        break;
                     
                }
             

            transfer.Korisnik = korisnik;
            transfer = k.PoveziSeNaServer(transfer);

            if (transfer.Ulogovan)
            {
                new FrmGlavna(k, transfer.Korisnik).ShowDialog();
            }
            else
            {
                MessageBox.Show(transfer.Poruka);
            }
        }
    }
}
