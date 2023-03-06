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
        protected static Journal journal;

        protected static string pathBase;
        protected static string pathLog;

        public List<Client> Clients;
        public List<Account> ClientsAccounts;

        /// <summary>
        /// Конструктор репозитория
        /// </summary>
        /// <param name="Path"></param>
        public Repository(string PathBase, string PathLog)
        {
            pathBase = PathBase;
            pathLog = PathLog;
            FileOper = new FileOperator(PathBase);
            journal = new Journal(PathLog, FileOper);
            FileOper.GetInfoFromFile();
            Clients = FileOper.ClientsBase;
            ClientsAccounts = FileOper.AccountsBase;
        }

        /// <summary>
        /// Создание репозитория
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public static Repository CreateRepository(string PathBase, string PathLog)
        {
            return new Repository(PathBase, PathLog);
        }

        /// <summary>
        /// Создание журнала записей об операциях
        /// </summary>
        /// <returns></returns>
        public static Journal CreateJournal()
        {
            return journal;
        }

        /// <summary>
        /// Открытие счета
        /// </summary>
        /// <param name="ClientId"></param>
        /// <param name="TypeAccount"></param>
        /// <returns></returns>
        public string openAccount(int ClientId, string TypeAccount, string Post)
        {
            string result = string.Empty;
            if (ClientId == 0)
            {
                return "Выберете клиента";
            }
            else
            {
                result = FileOper.openAccount(ClientId, TypeAccount, Post);
            }
            FileOper.GetInfoFromFile();
            return result;
        }

        /// <summary>
        /// Закрытие счета
        /// </summary>
        /// <param name="AccountToClose"></param>
        /// <returns></returns>
        public string closeAccount(Account AccountToClose, string Post)
        {
            string result = FileOper.closeAccount(AccountToClose, Post);
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
        public string transferSumBetweenAccounts(Account AccountToGetSum, Account AccountToPutSum, double Sum, string Post)
        {
            string result = FileOper.transferSumBetweenAccounts(AccountToGetSum, AccountToPutSum, Sum, Post);
            FileOper.GetInfoFromFile();
            return result;
        }

        /// <summary>
        /// Пополнение счета
        /// </summary>
        /// <param name="AccountToAddition"></param>
        /// <param name="Sum"></param>
        /// <returns></returns>
        public string additionAccount(Account AccountToAddition, double Sum, string Post)
        {
            string result = FileOper.additionAccount(AccountToAddition, Sum, Post);
            FileOper.GetInfoFromFile();
            return result;
        }
    }
}
