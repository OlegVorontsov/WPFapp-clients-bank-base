using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12._5_HomeWork_WPFapp_clients_bank_base
{
    abstract class Account
    {
        public int ClientId { get; private set; }
        public string typeAccount { get; set; }
        public double sum { get; set; }

        public Account(int ClientId, string TypeAccount, double Sum)
        {
            this.ClientId = ClientId;
            this.typeAccount = TypeAccount;
            this.sum = Sum;
        }
    }

    class Deposit : Account
    {
        public Deposit(int ClientId, string TypeAccount, double Sum) : base(ClientId, TypeAccount, Sum) { }
    }

    class Current : Account
    {
        public Current(int ClientId, string TypeAccount, double Sum) : base(ClientId, TypeAccount, Sum) { }
    }
}
