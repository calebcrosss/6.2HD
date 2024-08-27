using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7._1p
{
    internal class Account
    {
        private decimal _balance;
        public string _name;

        public Account(string name, decimal balance)
        {
            this._name = name;
            this._balance = balance;

        }


        public bool Deposit(decimal Amount)
        {
            if (Amount <= 0)
            {
                //Console.WriteLine("ERROR#1: The amount that you attempted to deposit was less than or equal to zero. ");
                return false;
            }
            else
            {
                this._balance += Amount;
                return true;
            }
        }

        public bool Withdrawl(decimal Amount)
        {
            if (Amount <= 0)
            {
                //Console.WriteLine("ERROR#2: The amount that you attempted to withdrawl was less than or equal to zero. ");
                return false;
            }
            else if (this._balance - Amount < 0)
            {
                //Console.WriteLine("ERROR#4: The amount that you attempted to withdrawl would make your balance negative. ");
                return false;
            }
            else
            {
                this._balance -= Amount;
                return true;
            }

        }
        public void Print()
        {
            Console.WriteLine(_balance.ToString("C"));
           // Console.WriteLine(_name);
        }
        public string Name()
        {
            return this._name;
        }
    }
}
