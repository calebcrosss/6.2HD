using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _7._1p
{
    internal class TransferTransaction : Transaction
    {
        private Account _fromAccount;
        private Account _toAccount;
        private DepositTransaction _deposit;       
        private WithdrawTransaction _withdraw;


        public override bool Success { get { return _deposit.Success && _withdraw.Success; } }
        public TransferTransaction(Account fromAccount, Account toAccount, decimal amount) : base(amount)
        {
            this._fromAccount = fromAccount;
            this._toAccount = toAccount;
            this._deposit = new DepositTransaction(toAccount, amount);
            this._withdraw = new WithdrawTransaction(fromAccount, amount);
        }
        


        public override void Print()
        {
            if (Success)
            {
                Console.WriteLine(_amount.ToString("C") + " has successfully been withdrawn from " + _fromAccount._name+ "'s account and transfered to " + _toAccount._name + "'s account");
            }
            else
            {
                Console.WriteLine("Transfer invalid, reversing transaction ");
            }
        }

        public override void Execute(bool showBal = true)
        {
            base.Execute();
            try
            {
                _withdraw.Execute();
            }
            catch (InvalidOperationException exception)
            {
                Console.WriteLine("The following error detected: " + exception.GetType().ToString() + " with message \"" + exception.Message + "\"");
            }
            if (_withdraw.Success)
            {
                try
                {
                    //when showBal bool is true (which is its default value) it will show recipiants balance as we are re-using the deposit method, where it is favourable to show your balance after the deposit
                    showBal = false;
                    _deposit.Execute(showBal);
                    Print();
                }
                catch (InvalidOperationException exception)
                {
                    Console.WriteLine("The following error detected: " + exception.GetType().ToString() + " with message \"" + exception.Message + "\"");
                    _withdraw.Rollback();
                }

            }

            _success = true;     
        }

        public override void Rollback()
        {
            if (!Success)
            {
                throw new InvalidOperationException("ERROR#10: Transaction was not successful, rollback unavaliable. ");
            }
            if (Reversed)
            {
                throw new InvalidOperationException("ERROR#8: Transfer transaction has already been reversed");
            }
            if (_toAccount.Withdrawl(_amount) && _fromAccount.Deposit(_amount))
            {
                Reversed = true;
                _fromAccount.Print();
                _toAccount.Print();
            }
            else
            {
                throw new InvalidOperationException("ERROR#12: Insufficient funds");
            }

        }


    }
}
