using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _7._1p
{
    internal class DepositTransaction : Transaction
    {
        private Account _account; //account is the only variable not in base class


        public override bool Success { get { return _success; } } //to fix Compiler Error CS0534

        public DepositTransaction(Account account, decimal amount) : base(amount)
        {
            this._account = account;
        }
        
        public override void Print()
        {
            if (Success)
            {
                Console.WriteLine(_amount.ToString("C") + " has successfully been deposited to " + _account._name + "'s account ");
            }
            else
            {
                Console.WriteLine("Deposit invalid, reversing transaction ");
            }

        }

        public override void Execute(bool showBal = true)  
        {
            base.Execute();           
            
            _success = _account.Deposit(_amount);
            if (!_success)
            {
                throw new InvalidOperationException("ERROR#1: The amount that you attempted to deposit was less than or equal to zero.");
            }
            if (showBal) //will display the balance only if you chose, (when re-using for transfers we dont want to see the other person's balance)
            {
                Console.WriteLine(_account._name + "'s new balance is: ");
                _account.Print();
            }
                        

        }

        public override void Rollback()
        {
            base.Rollback();
            bool possible = _account.Withdrawl(_amount);
            
            if (!possible)
            {
                throw new InvalidOperationException("ERROR#7: Invald rollback amount! (Either will put your balance below zero or you are rolling back an amount less than or equal to zero) ");
            }
            base.Reversed = possible;


        }


    }
}
