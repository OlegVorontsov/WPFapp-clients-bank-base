using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12._5_HomeWork_WPFapp_clients_bank_base
{
    class Account
    {
        public int clientId { get; private set; }
        public int accountId { get; set; }
        public string typeAccount { get; set; }
        public double sum { get; set; }

        private static Random rand;

        private static int randomID()
        {
            return rand.Next(9999);
        }

        public Account(int ClientId, int AccountId, string TypeAccount, double Sum)
        {
            rand = new Random();
            this.clientId = ClientId;
            if (AccountId == 0)
            {
                this.accountId = randomID();
            }
            else
            {
                this.accountId = AccountId;
            }
            this.typeAccount = TypeAccount;
            this.sum = Sum;
        }
    }


    class Deposit : Account
    {
        public Deposit(int ClientId, int AccountId, string TypeAccount, double Sum) : base(ClientId, AccountId, TypeAccount, Sum) { }
    }

    class Current : Account
    {
        public Current(int ClientId, int AccountId, string TypeAccount, double Sum) : base(ClientId, AccountId, TypeAccount, Sum) { }
    }
}
