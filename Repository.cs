using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12._5_HomeWork_WPFapp_clients_bank_base
{
    class Repository
    {
        protected static FileOperator FileOper;
        protected static string path;

        public List<Client> Clients;
        public List<Account> ClientsAccounts = new List<Account>();

        /// <summary>
        /// Конструктор репозитория
        /// </summary>
        /// <param name="Path"></param>
        public Repository(string Path)
        {
            path = Path;
            FileOper = new FileOperator(Path);
            FileOper.GetInfoFromFile();
            Clients = FileOper.ClientsBase;
            ClientsAccounts = FileOper.AccountsBase;
        }

        /// <summary>
        /// Создание репозитория
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public static Repository CreateRepository(string Path)
        {
            return new Repository(Path);
        }

        /// <summary>
        /// Открытие счета
        /// </summary>
        /// <param name="ClientId"></param>
        /// <param name="TypeAccount"></param>
        /// <returns></returns>
        public string openAccount(int ClientId, string TypeAccount)
        {
            string result = string.Empty;
            if (ClientId == 0)
            {
                return "Выберете клиента";
            }
            else
            {
                result = FileOper.openAccount(ClientId, TypeAccount);
            }
            FileOper.GetInfoFromFile();
            return result;
        }

        /// <summary>
        /// Закрытие счета
        /// </summary>
        /// <param name="AccountToClose"></param>
        /// <returns></returns>
        public string closeAccount(Account AccountToClose)
        {
            string result = FileOper.closeAccount(AccountToClose);
            FileOper.GetInfoFromFile();
            return result;
        }

        /// <summary>
        /// Перевод между счетами
        /// </summary>
        /// <param name="AccountToGetSum"></param>
        /// <param name="AccountToPutSum"></param>
        /// <param name="Sum"></param>
        /// <returns></returns>
        public string transferSumBetweenAccounts(Account AccountToGetSum, Account AccountToPutSum, double Sum)
        {
            string result = FileOper.transferSumBetweenAccounts(AccountToGetSum, AccountToPutSum, Sum);
            FileOper.GetInfoFromFile();
            return result;
        }
    }
}
