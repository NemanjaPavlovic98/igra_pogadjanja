using Domen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    public class Server
    {
        Socket listener;
        public static List<Korisnik> listaKorisnika = new List<Korisnik>();

        public bool PokreniServer()
        {
            try
            {
                listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ep = new IPEndPoint(IPAddress.Any, 9999);
                listener.Bind(ep);

                Thread threadKlijent = new Thread(Osluskuj);
                threadKlijent.IsBackground = true;
                threadKlijent.Start();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        void Osluskuj()
        {
            try
            {
                while (true)
                {
                    listener.Listen(5);
                    Socket klijent = listener.Accept();
                    NetworkStream stream = new NetworkStream(klijent);
                    BinaryFormatter formatter = new BinaryFormatter();

                    TransferKlasa transfer = (TransferKlasa)formatter.Deserialize(stream);

                    Korisnik k = transfer.Korisnik;
                    transfer.Ulogovan = true;

                    if (listaKorisnika.Contains(k))
                    {
                        transfer.Ulogovan = false;
                        transfer.Poruka = "Vec si ulogovan";
                    }

                    if (transfer.Ulogovan && !k.Mail.Contains('@'))
                    {
                        transfer.Ulogovan = false;
                        transfer.Poruka = "Fali @";
                    }

                    if (transfer.Ulogovan && k.Password == "")
                    {
                        transfer.Ulogovan = false;
                        transfer.Poruka = "Niste uneli sifru";
                    }

                    if (transfer.Ulogovan && !Char.IsDigit(k.Password[0]))
                    {
                        transfer.Ulogovan = false;
                        transfer.Poruka = "Lozinka mora poceti cifrom";
                    }

                    if (transfer.Ulogovan)
                    {
                        bool postoji = false;
                        foreach (Char c in k.Password)
                        {
                            if (Char.IsDigit(c))
                            {
                                postoji = true;
                                break;
                            }
                        }
                        if (!postoji)
                        {
                            transfer.Ulogovan = false;
                            transfer.Poruka = "Nema broja u sifri";
                        }
                    }

                    if (transfer.Ulogovan)
                    {
                        DodeliMatricu(k);
                        new NitKlijenta(stream, k);
                        listaKorisnika.Add(k);
                    }
                    transfer.Korisnik = k;
                    formatter.Serialize(stream, transfer);

                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Greska: " + ex.Message); ;
            }
        }

        public bool ZaustaviServer()
        {
            try
            {
                listener.Close();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        void DodeliMatricu(Korisnik k)
        {
            Random r = new Random();
            int i = 0;
            int j = 0;
            int z = 0;
            k.Matrica = new string[k.Dimenzija, k.Dimenzija];
            switch (k.Dimenzija)
            {
                case 3:
                    while (z != 3)
                    {
                        i = r.Next(0, 3);
                        j = r.Next(0, 3);

                        if (k.Matrica[i, j] != "*") k.Matrica[i, j] = "*";
                        else continue;

                        z++;
                    }
                    k.MaxBrPokusaja = 5;
                    break;
                case 4:
                    while (z != 3)
                    {
                        i = r.Next(0, 4);
                        j = r.Next(0, 4);

                        if (k.Matrica[i, j] != "*") k.Matrica[i, j] = "*";
                        else continue;

                        z++;
                    }
                    k.MaxBrPokusaja = 9;
                    break;
                case 5:
                    while (z != 3)
                    {
                        i = r.Next(0, 5);
                        j = r.Next(0, 5);

                        if (k.Matrica[i, j] != "*") k.Matrica[i, j] = "*";
                        else continue;

                        z++;
                    }
                    k.MaxBrPokusaja = 13;
                    break;
                default:
                    break;
            }
        }

    }
}
