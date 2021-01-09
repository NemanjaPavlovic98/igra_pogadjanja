using Domen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    public class NitKlijenta
    {
        private NetworkStream stream;
        private Korisnik k;
        BinaryFormatter formatter;

        public NitKlijenta(NetworkStream stream, Korisnik k)
        {
            this.stream = stream;
            this.k = k;
            formatter = new BinaryFormatter();
            Thread threadKlijent = new Thread(Obradi);
            threadKlijent.IsBackground = true;
            threadKlijent.Start();
        }

        void Obradi()
        {
            try
            {
                int operacija = 0;
                while (operacija != (int)Operacije.Kraj)
                {
                    TransferKlasa transfer = (TransferKlasa)formatter.Deserialize(stream);

                    switch (transfer.Operacija)
                    {
                        case Operacije.Pogadjaj:
                            k.BrojPokusaka++;

                            if (k.Matrica[transfer.I, transfer.Y] == "*")
                            {
                                transfer.Pogodjen = true;
                                k.BrojPogodata++;
                                k.Matrica[transfer.I, transfer.Y] = "";
                            }
                            else transfer.Pogodjen = false;

                            if(k.BrojPogodata == 3)
                            {
                                transfer.Poruka = "Pobedili ste!";
                                transfer.Kraj = true;
                            }

                            if (!transfer.Kraj && k.BrojPokusaka==k.MaxBrPokusaja)
                            {
                                transfer.Poruka = "Nemate vise pokusaja!";
                                transfer.Kraj = true;
                            }

                            formatter.Serialize(stream, transfer);
                            break;
                        case Operacije.Kraj:
                            Server.listaKorisnika.Remove(k);
                            operacija = 1;

                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception)
            {
                Server.listaKorisnika.Remove(k);
                return;
            }
        }
        }
    }
