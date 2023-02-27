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
        public List<Account> AccountsBase = new List<Account>();

        public FileOperator(string Path)
        {
            path = Path;
        }

        /// <summary>
        /// Получение данных из файла
        /// </summary>
        public void GetInfoFromFile()
        {
            ClientsBase.Clear();
            AccountsBase.Clear();
            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    string[] args = sr.ReadLine().Split('#');

                    Client newClient = new Client(int.Parse(args[0]), args[1], args[2], args[3]);

                    ClientsBase.Add(newClient);

                    string[] sums = sr.ReadLine().Split('#');

                    for (int i = 0; i < sums.Length; i+=4)
                    {
                        if (sums[i+2] == "депозит")
                        {
                            AccountsBase.Add(new Deposit(int.Parse(sums[i]), int.Parse(sums[i + 1]), sums[i + 2], double.Parse(sums[i + 3])));
                        }
                        else if (sums[i+2] == "текущий")
                        {
                            AccountsBase.Add(new Current(int.Parse(sums[i]), int.Parse(sums[i + 1]), sums[i + 2], double.Parse(sums[i + 3])));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Запись данных в файл
        /// </summary>
        public void PutInfoIntoFile()
        {
            File.Delete(path);
            File.Create(path).Close();
            using (StreamWriter sw = new StreamWriter(path, true, Encoding.UTF8))
            {
                for (int i = 0; i < ClientsBase.Count; i++)
                {
                    string lineClient = string.Empty;
                    lineClient = $"{ClientsBase[i].clientId}#" +
                                 $"{ClientsBase[i].surname}#" +
                                 $"{ClientsBase[i].name}#" +
                                 $"{ClientsBase[i].patronymic}";
                    sw.WriteLine(lineClient);

                    string lineAccounts = string.Empty;
                    for (int j = 0; j < AccountsBase.Count; j++)
                    {
                        string Account = string.Empty;
                        if (ClientsBase[i].clientId == AccountsBase[j].clientId)
                        {
                            Account = $"{AccountsBase[j].clientId}#" +
                                      $"{AccountsBase[j].accountId}#" +
                                      $"{AccountsBase[j].typeAccount}#" +
                                      $"{AccountsBase[j].sum}#";
                            lineAccounts += Account;
                        }
                    }
                    sw.WriteLine(lineAccounts.TrimEnd('#'));
                }
            }
        }

        /// <summary>
        /// Открытие счета
        /// </summary>
        /// <param name="ClientId"></param>
        /// <param name="TypeAccount"></param>
        /// <returns></returns>
        public string openAccount(int ClientId, string TypeAccount)
        {
            if (TypeAccount == "депозит")
            {
                AccountsBase.Add(new Deposit(ClientId, 0, TypeAccount, 0));
            }
            else if (TypeAccount == "текущий")
            {
                AccountsBase.Add(new Current(ClientId, 0, TypeAccount, 0));
            }
            PutInfoIntoFile();
            return "Счет открыт";
        }

        /// <summary>
        /// Закрытие счета
        /// </summary>
        /// <param name="AccountToClose"></param>
        /// <returns></returns>
        public string closeAccount(Account AccountToClose)
        {
            AccountsBase.Remove(AccountToClose);
            PutInfoIntoFile();
            return "Счет закрыт";
        }

        public string transferSumBetweenAccounts(Account AccountToGetSum, Account AccountToPutSum, double Sum)
        {
            return "Деньги переведены";
        }
    }
}
