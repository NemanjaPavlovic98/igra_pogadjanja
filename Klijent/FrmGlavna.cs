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
    public partial class FrmGlavna : Form
    {
        private Komunikacija k;
        private Korisnik korisnik;

        public object Operacija { get; private set; }

        public FrmGlavna()
        {
           
        }

        public FrmGlavna(Komunikacija k, Korisnik korisnik)
        {
            InitializeComponent();
            this.k = k;
            this.korisnik = korisnik;
        }
        void PostaviPoligon()
        {
            for (int i = 0; i < korisnik.Dimenzija; i++)
            {
                DataGridViewTextBoxColumn kolona = new DataGridViewTextBoxColumn();
                dataGridView1.Columns.Add(kolona);
            }

            dataGridView1.Rows.Add(korisnik.Dimenzija);

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Height = (dataGridView1.Height / korisnik.Dimenzija) - 1;
            }
            {

            }
              
        }

        private void FrmGlavna_Load(object sender, EventArgs e)
        {
            PostaviPoligon();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            TransferKlasa transfer = new TransferKlasa();
            transfer.Korisnik = korisnik;
            transfer.Operacija = Operacije.Pogadjaj;
            transfer.I = dataGridView1.CurrentCell.RowIndex;
            transfer.Y = dataGridView1.CurrentCell.ColumnIndex;

            transfer = k.Pogadjaj(transfer);

            if(transfer.Pogodjen == true)
            {
                dataGridView1.CurrentCell.Value = "*";
                dataGridView1.CurrentCell.Style.BackColor = Color.LightBlue;
            }
            else
            {
                dataGridView1.CurrentCell.Style.BackColor = Color.Red;

            }

            //dataGridView1.CurrentCell.Selected = false;
            if (transfer.Kraj)
            {
                MessageBox.Show(transfer.Poruka);
                this.Close();
            }
        }

        private void FrmGlavna_FormClosed(object sender, FormClosedEventArgs e)
        {
            k.Kraj();
        }
    }
}
