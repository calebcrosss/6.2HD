using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _7._1p
{
    internal class WithdrawTransaction : Transaction
    {
        private Account _account;

        public override bool Success { get { return _success; } }
                
        public WithdrawTransaction(Account account, decimal amount) : base(amount) 
        {
            this._account = account;    
        }




        public override void Print()
        {
            if (_success)
            {
                Console.WriteLine(_amount.ToString("C") + " has successfully been withdrawn from " + _account._name + "'s account ");
            }
            else
            {
                Console.WriteLine("Withdraw invalid, reversing transaction ");
            }

        }

        public override void Execute(bool showBal = true)
        {
            base.Execute();
            _success = _account.Withdrawl(_amount);
            if (!_success)
            {
                throw new InvalidOperationException("ERROR#11: Invalid withdrawl amount!");
            }
            Console.WriteLine(_account._name + "'s new balance is:");
            _account.Print();


        }

        public override void Rollback()
        {
            base.Rollback();
            bool possible = _account.Deposit(_amount);

            if (!possible)
            {
                throw new InvalidOperationException("ERROR#7: Amout is less than or equal to zero.");
            }

            base.Reversed = possible;

        }


    }
}
