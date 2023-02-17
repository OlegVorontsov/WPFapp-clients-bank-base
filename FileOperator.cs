using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12._5_HomeWork_WPFapp_clients_bank_base
{
    class FileOperator
    {
        protected static string path;
        public List<Client> ClientsBase = new List<Client>();

        public FileOperator(string Path)
        {
            path = Path;
        }

        public void GetInfoFromFile()
        {
            ClientsBase.Clear();
            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    string[] args = sr.ReadLine().Split('#');

                    Client newClient = new Client(int.Parse(args[0]), args[1], args[2], args[3]);

                    ClientsBase.Add(newClient);

                    string[] sums = sr.ReadLine().Split('#');

                    for (int i = 0; i < sums.Length; i+=3)
                    {
                        if (sums[i+1] == "депозит")
                        {
                            newClient.clientAccounts.Add(new Deposit(int.Parse(sums[i]), sums[i + 1], double.Parse(sums[i + 2])));
                        }
                        else if (sums[i+1] == "текущий")
                        {
                            newClient.clientAccounts.Add(new Current(int.Parse(sums[i]), sums[i + 1], double.Parse(sums[i + 2])));
                        }
                    }
                }
            }
        }
    }
}
