using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12._5_HomeWork_WPFapp_clients_bank_base
{
    class Client
    {
        public int clientId { get; set; }
        public string surname { get; set; }
        public string name { get; set; }
        public string patronymic { get; set; }

        public Client(int ClientId, string Surname, string Name, string Patronymic)
        {
            this.clientId = ClientId;
            this.surname = Surname;
            this.name = Name;
            this.patronymic = Patronymic;
        }

    }
}
