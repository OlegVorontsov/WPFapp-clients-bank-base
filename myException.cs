using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12._5_HomeWork_WPFapp_clients_bank_base
{
    public class MyException : Exception
    {
        public MyException(string Msg) : base (Msg)
        {

        }
    }
}
