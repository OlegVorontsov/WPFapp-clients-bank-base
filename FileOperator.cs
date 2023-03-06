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

        public event Action<Account, string> AccountWasOpen;
        public event Action<Account, string> AccountWasClosed;
        public event Action<Account, double, string> AccountWasRefilled;
        public event Action<Account, Account, double, string> TransferWasDone;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="Path"></param>
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
        public string openAccount(int ClientId, string TypeAccount, string Post)
        {
            if (TypeAccount == "депозит")
            {
                Account newAccount = new Deposit(ClientId, 0, TypeAccount, 0);
                AccountsBase.Add(newAccount);
                AccountWasOpen?.Invoke(newAccount, Post);
            }
            else if (TypeAccount == "текущий")
            {
                Account newAccount = new Current(ClientId, 0, TypeAccount, 0);
                AccountsBase.Add(newAccount);
                AccountWasOpen?.Invoke(newAccount, Post);
            }
            PutInfoIntoFile();
            return "Счет открыт";
        }

        /// <summary>
        /// Закрытие счета
        /// </summary>
        /// <param name="AccountToClose"></param>
        /// <returns></returns>
        public string closeAccount(Account AccountToClose, string Post)
        {
            AccountsBase.Remove(AccountToClose);
            AccountWasClosed?.Invoke(AccountToClose, Post);
            PutInfoIntoFile();
            return "Счет закрыт";
        }

        /// <summary>
        /// Перевод между счетами
        /// </summary>
        /// <param name="AccountToGetSum"></param>
        /// <param name="AccountToPutSum"></param>
        /// <param name="Sum"></param>
        /// <returns></returns>
        public string transferSumBetweenAccounts(Account AccountToGetSum, Account AccountToPutSum, double Sum, string Post)
        {
            if ((AccountToGetSum.sum - Sum) >= 0)
            {
                AccountToGetSum.sum -= Sum;
                AccountToPutSum.sum += Sum;
                TransferWasDone?.Invoke(AccountToGetSum, AccountToPutSum, Sum, Post);
                PutInfoIntoFile();
                return "Средства переведены";
            }
            else
            {
                return "Недостаточно средств";
            }
        }

        /// <summary>
        /// Пополнение счета
        /// </summary>
        /// <param name="AccountToAddition"></param>
        /// <param name="Sum"></param>
        /// <returns></returns>
        public string additionAccount(Account AccountInWork, double Sum, string Post)
        {
            AccountInWork.sum += Sum;
            AccountWasRefilled?.Invoke(AccountInWork, Sum, Post);
            PutInfoIntoFile();
            return "Счет пополнен";
        }
    }
}
