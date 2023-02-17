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
        public List<Account> Accounts;

        public Repository(string Path)
        {
            path = Path;
            FileOper = new FileOperator(Path);
            FileOper.GetInfoFromFile();
            Clients = FileOper.ClientsBase;
            foreach (var item in Clients)
            {
                Accounts.Add(item.clientAccounts);
            }
        }

        public static Repository CreateRepository(string Path)
        {
            return new Repository(Path);
        }
    }
}
