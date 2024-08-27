using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7._1p
{
    public abstract class Transaction
    {
        protected decimal _amount;
        protected bool _success;
        private bool _executed;
        private bool _reversed;
        private DateTime _dateStamp;

        public bool Executed { get { return _executed; } }

        public abstract bool Success { get;  }

        public bool Reversed { get { return _reversed; } set { _reversed = value; } } 
        public DateTime DateStamp { get { return _dateStamp; } }

        public Transaction (decimal amount)
        {
            if (amount > 0)
            {
                this._amount = amount;
            }
            else
            {
                _amount = 0;
                throw new InvalidOperationException("ERROR#6: Amount is less than or equal to 0");
            }
        }

        public abstract void Print();

        public virtual void Execute(bool showBal = true)
        {
            if (_executed)
            {
                throw new InvalidOperationException("ERROR#4: Deposit already executed");
            }
            _executed = true;
            _dateStamp = DateTime.Now;
            
        }

        public virtual void Rollback()
        {
            if (!_success)
            {
                throw new InvalidOperationException("ERROR#5: Transaction was not successful, rollback unavaliable. ");
            }
            if (_reversed)
            {
                throw new InvalidOperationException("ERROR#3: Transaction has already been reversed");
            }
            _dateStamp = DateTime.Now;
        }
    }

}

