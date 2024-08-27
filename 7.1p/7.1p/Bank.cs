using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace _7._1p
{
    internal class Bank
    {
        private List<Account> _accounts;
        private List<Transaction> _transactions;

        public List<Transaction> Transaction { get { return _transactions; } }
        
        public Bank() 
        {
            _accounts = new List<Account>();
            _transactions = new List<Transaction>();
        }
        public void AddAccount(Account account)
        {
            _accounts.Add(account);
        }
        public Account GetAccount (String name)
        {
            for (int i = 0; i < _accounts.Count; i++)
            {
                if (_accounts[i]._name.ToUpper() == name.ToUpper())
                {
                    return _accounts[i];
                }
            }
            return null;
        }

        public void ExecuteTransaction(Transaction transaction)
        {
            _transactions.Add(transaction);
            try
            {
                transaction.Execute();
            }
            catch (InvalidOperationException exception)
            {
                Console.WriteLine("The following error detected: " + exception.GetType().ToString() + " with message \"" + exception.Message + "\"");
            }

        }

        public void RollBackTransaction(Transaction transaction)
        {
            try
            {
                transaction.Rollback();
            }
            catch (InvalidOperationException exception)
            {
                Console.WriteLine("The following error detected: " + exception.GetType().ToString() + " with message \"" + exception.Message + "\"");
            }
        }

        public string Type(Transaction transaction)
        {
            switch (transaction.GetType().ToString())
            {
                case "_7._1p.WithdrawTransaction":
                    return "Withdraw";

                case "_7._1p.DepositTransaction":
                    return "Deposit";

                case "_7._1p.TransferTransaction":
                    return "Transfer";
            }
            return "N/A";
        }
        public string Status(Transaction transaction)
        {
            if (transaction.Reversed == true)
            {
                return "Rolled back";
            }
            if (transaction.Success == true)
            {
                return "Successful";
            }
            if (transaction.Success != true)
            {
                return "Failed";
            }
            return "N/A";
        }
        public void PrintTransactionHistory()
        {
            if (_transactions.Count < 1) 
            {
                throw new InvalidOperationException("There is no history to view!");
            }

            Console.WriteLine("| {0,2} |{1,-25} | {2,-15}| {3,15} |", "#", "DateTime", "Type", "Status");
            Console.WriteLine(new string('-', 69));

            for (int i = 0; i < _transactions.Count; i++)
            {
                Console.WriteLine("| {0,2} |{1,-25} | {2,-15}| {3,15} |", i+1, _transactions[i].DateStamp, Type(_transactions[i]), Status(_transactions[i]));
            }
            
        }
        
    }
}
