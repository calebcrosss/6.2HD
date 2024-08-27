using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace _7._1p
{
    class Outside
    {
        public enum MenuOption
        {
            Withdrawl,
            Deposit,
            Transfer,
            AddAccount,
            FindAccount,
            Print,
            PrintTransactionHistory,
            Quit
        }

    }

    internal class BankSystem
    {
        static int ReadInt(string prompt)
        {
            int result;
            Console.WriteLine(prompt);
            while (!int.TryParse(Console.ReadLine(), out result))
            {
                Console.WriteLine("Please enter a whole number.");
                Console.WriteLine(prompt);
            }
            return result;
        }

        //modified read int range which will output menu option instead
        static Outside.MenuOption ReadUserOption(string prompt, int min, int max)
        {
            int result;
            result = ReadInt(prompt);
            while (result < min || result > max)
            {
                Console.WriteLine("Please enter a number between " + min.ToString() + " and " + max.ToString());
                result = ReadInt(prompt);
            }

            result -= 1;

            Outside.MenuOption output = (Outside.MenuOption)result;
            return output;

        }
        static string ReadString(string prompt)
        {
            string result;
            Console.WriteLine(prompt);
            result = Console.ReadLine();
            return result;
        }

        static decimal ReadDecimal(string prompt)
        {
            decimal result;
            Console.WriteLine(prompt);
            while (!decimal.TryParse(Console.ReadLine(), out result))
            {
                Console.WriteLine("Please enter a number.");
                Console.WriteLine(prompt);
            }
            return result;
        }
        static decimal ReadDecimalRange(string prompt, int min, int max = 0)
        {
            decimal result;
            result = ReadDecimal(prompt);
            if (max != 0)
            {
                while (result < min || result > max)
                {
                    Console.WriteLine("Please enter a number between " + min.ToString() + " and " + max.ToString());
                    result = ReadDecimal(prompt);
                }

            }
            else
            {
                while (result <= min)
                {
                    Console.WriteLine("Please enter a number above " + min.ToString());
                    result = ReadDecimal(prompt);
                }
            }

            return result;
        }

        static void DoWithdrawl(Bank bank)
        {
            Account account = FindAccount(bank, ReadString("What account would you like to withdraw from? "));
            if (account != null)
            {
                decimal withdrawAmmount = ReadDecimalRange("How much money would you like to withdraw?", 0);
                WithdrawTransaction withdrawTransaction = new WithdrawTransaction(account, withdrawAmmount);
                bank.ExecuteTransaction(withdrawTransaction);
                withdrawTransaction.Print();
            }
  
        }

        static void DoDeposit(Bank bank)
        {
            Account account = FindAccount(bank, ReadString("What account would you like to deposit to?"));
            if (account != null)
            {
                decimal depositAmount = ReadDecimalRange("How much money would you like to deposit?", 0);
                DepositTransaction deopsitTransaction = new DepositTransaction(account, depositAmount);
                bank.ExecuteTransaction(deopsitTransaction);
                deopsitTransaction.Print();

            }
            
        }

        static void DoTransfer(Bank bank)
        {
            Account account = FindAccount(bank, ReadString("What is the account you want to trasnfer from?"));
            if (account != null)
            {
                Account account2 = FindAccount(bank, ReadString("What is the account you want to transfer to? "));
                if (account2 != null)
                {
                    decimal withdrawAmount = ReadDecimalRange("How much money would you like to transfer?", 0);
                    TransferTransaction transferTransaction = new TransferTransaction(account, account2, withdrawAmount);
                    bank.ExecuteTransaction(transferTransaction);
                }
            }
        }

        static void DoRollback(Bank bank, Transaction transaction) 
        {
            bank.RollBackTransaction(transaction);
        }

        static void DoPrint(Account Account)
        {
            Account.Print();
        }

        static public void PrintMenu()
        {
            Console.WriteLine("---- Banking Menu ----");
            Console.WriteLine("1. Withdrawl \n" + "2. Deposit \n" + "3. Transfer \n" + "4. Add a new account \n" + "5. Find account \n" + "6. Print \n" + "7. Print Transaction History \n" + "8. Quit \n");
        }

        public static bool ConfirmChoice(Outside.MenuOption menuOption)
        {
            Console.Write("You are selecting option: " + menuOption + ". Are you sure you want to select this? ");
            string confirm = ReadString("(Yes/No)").ToLower();

            //input validation on confirmation
            while (confirm != "yes" && confirm != "y" && confirm != "no" && confirm != "n")
            {
                Console.WriteLine("ERROR#3: Response must be either 'yes', 'y', 'no' or 'n'");
                confirm = ReadString("(Yes/No)").ToLower();

            }
            if (confirm == "yes" || confirm == "y")
            {
                return true;
            }
            return false;


        }

        public static bool Confirm()
        {
            string confirm = ReadString("(Yes/No)").ToLower();

            //input validation on confirmation
            while (confirm != "yes" && confirm != "y" && confirm != "no" && confirm != "n")
            {
                Console.WriteLine("ERROR#3: Response must be either 'yes', 'y', 'no' or 'n'");
                confirm = ReadString("(Yes/No)").ToLower();

            }
            if (confirm == "yes" || confirm == "y")
            {
                return true;
            }
            return false;
        }
        static int ReadIntRange(string prompt, int min, int max)
        {
            int result;
            result = ReadInt(prompt);
            while (result < min || result > max)
            {
                Console.WriteLine("Please enter a number between " + min.ToString() + " and " + max.ToString());
                result = ReadInt(prompt);
            }
            return result;
        }

        static void Menu(Bank bank)
        {
            bool quit = false;
            while (!quit)
            {                
                bool confirmation = false;
                while (!confirmation)
                {

                    //menu display
                    PrintMenu();
                    //reads user option with validation and subtracts one so selection reflects the menu
                    Outside.MenuOption MenuOption = ReadUserOption("Select an option: ", 1, 9);

                    if (ConfirmChoice(MenuOption))
                    {
                        switch (MenuOption)
                        {
                            case Outside.MenuOption.Withdrawl:
                                try
                                {
                                    DoWithdrawl(bank);
                                }
                                catch (InvalidOperationException exception)
                                {
                                    Console.WriteLine("The following error detected: " + exception.GetType().ToString() + " with message \"" + exception.Message + "\"");
                                }
                                break;

                            case Outside.MenuOption.Deposit:
                                try
                                {
                                    DoDeposit(bank);
                                }
                                catch (InvalidOperationException exception)
                                {
                                    Console.WriteLine("The following error detected: " + exception.GetType().ToString() + " with message \"" + exception.Message + "\"");
                                }
                                
                                break;

                            case Outside.MenuOption.Transfer:
                                try
                                {
                                    DoTransfer(bank);
                                }
                                catch (InvalidOperationException exception)
                                {
                                    Console.WriteLine("The following error detected: " + exception.GetType().ToString() + " with message \"" + exception.Message + "\"");
                                }
                                break;

                            case Outside.MenuOption.AddAccount:
                                Account newAccount = new Account(ReadString("What is the account name? "), ReadDecimalRange("What is the starting balance?",0));
                                bank.AddAccount(newAccount);
                                break;

                            case Outside.MenuOption.FindAccount:
                                string accToFind = ReadString("What account would you like to find");
                                FindAccount(bank, accToFind);
                                break;

                            case Outside.MenuOption.Print:
                                accToFind = ReadString("What account would you like to print");
                                DoPrint(FindAccount(bank, accToFind));
                                break;

                            case Outside.MenuOption.PrintTransactionHistory:
                                try
                                {
                                    bank.PrintTransactionHistory();
                                    Console.Write("Would you like to rollback a transaction? ");
                                    if (Confirm())
                                    {
                                        DoRollback(bank, bank.Transaction[ReadIntRange("What is the index number of the transaction?", 1, bank.Transaction.Count) - 1]);
                                    }


                                }
                                catch (InvalidOperationException exception)
                                {
                                    Console.WriteLine("The following error detected: " + exception.GetType().ToString() + " with message \"" + exception.Message + "\"");
                                }
                                break;

                            case Outside.MenuOption.Quit:
                                quit = true;
                                break;

                        }
                        confirmation = true;
                    }
                }


            }
        }

        private static Account FindAccount (Bank bank, string accToFind) 
        {
            if (bank.GetAccount(accToFind) != null)
            {
                Console.WriteLine("Account Found!");
            }
            else
            {
                Console.WriteLine("ERROR#12: That account does not exist");
            }
            return bank.GetAccount(accToFind);
        }

        static void Main()
        {
            Account calebAccount = new Account("Caleb", 200);
            Account joelAccount = new Account("Joel", 100);
            Bank bank = new Bank();
            bank.AddAccount(joelAccount);
            bank.AddAccount(calebAccount);
            Menu(bank);

            

        }

    }




}


