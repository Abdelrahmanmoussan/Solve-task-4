using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Solve_task_4__27_11_24_
{
    public class Account
    {
        public string Name { get; set; }
        public double Balance { get; set; }

        public Account(string Name = "Unnamed Account", double Balance = 0.0)
        {
            this.Name = Name;
            this.Balance = Balance;
        }

        public virtual bool Deposit(double amount)
        {
            if (amount > 0)
            {
                Balance += amount;
                return true;
            }
            return false;
        }

        

        public virtual bool Withdraw(double amount)
        {
            if (Balance - amount >= 0)
            {
                Balance -= amount;
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return $"Name is: {Name}, Balanse is: {Balance}";
        }
    }

    //Saving Account calss
    public class SavingAccount : Account 
    {
        public SavingAccount(string Name = "Unnamed Account", double Balance = 0.0, double rate=0.02) : base(Name, Balance)
        {
            Rate = rate;
        }

        public double Rate { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()}, Rate: {Rate}";
        }

        public override bool Withdraw(double amount)
        {
            return base.Withdraw(amount + Rate);
        }

    }

    //Checking Account calss
    public class CheckingAccount : Account
    {
        public double fee { get; set; }

        public CheckingAccount(string Name = "Unnamed Account", double Balance = 0.0, double fee=1.5) : base(Name, Balance) 
        {
            this.fee = fee;
        }

        
        public override bool Withdraw(double amount)
        {
            return base.Withdraw(amount + fee);
        }

        public override string ToString()
        {
            return $"{base.ToString()}, fee: {fee}";
        }

    }

    /// <summary>
    /// in TrustAccount class iam try to do what was required in task
    /// </summary>

    //Trust Account class
    public class TrustAccount : SavingAccount
    {
        private int WithdrawCount { get; set; }
        public const int MaxWithdraw = 3;
        public const int MaxWithdrawPerecentage = 20/100;

        public TrustAccount(string Name = "Unnamed Account", double Balance = 0.0, double rate = 0.02)
            : base(Name, Balance, rate) { }

        public override bool Deposit(double amount)
        {
            if (amount >= 5000)
            {
                amount += 50; 
            }
            return base.Deposit(amount);
        }



        public override bool Withdraw(double amount)
        {
            if (WithdrawCount >= MaxWithdraw || amount > Balance * MaxWithdrawPerecentage)
            {
                return false; 
            }
            if (base.Withdraw(amount))
            {
                WithdrawCount++;
                return true;
            }
            return false;
        }


        public override string ToString()
        {
            return $"{base.ToString()}";
        }

    }


    public static class AccountUtil 
    {
        // Utility helper functions for Account class

        public static void Display(List<Account> accounts) 
        {
            Console.WriteLine("\n=== Accounts ==========================================");
            foreach (var acc in accounts)
            {
                Console.WriteLine(acc);
            }
        }

        public static void Deposit(List<Account> accounts, double amount)
        {
            Console.WriteLine("\n=== Depositing to Accounts =================================");
            foreach (var acc in accounts)
            {
                if (acc.Deposit(amount))
                    Console.WriteLine($"Deposited {amount} to {acc}");
                else
                    Console.WriteLine($"Failed Deposit of {amount} to {acc}");
            }
        }

        public static void Withdraw(List<Account> accounts, double amount)
        {
            Console.WriteLine("\n=== Withdrawing from Accounts ==============================");
            foreach (var acc in accounts)
            {
                if (acc.Withdraw(amount))
                    Console.WriteLine($"Withdrew {amount} from {acc}");
                else
                    Console.WriteLine($"Failed Withdrawal of {amount} from {acc}");
            }
        }
    }





    /// <summary>
    /// in main i change List< SavingAccount > to List < Account >
    /// in main i change List< CheckingAccount > to List < Account >
    /// in main i change List< TrustAccount > to List < Account >
    /// because in AccountUtil class  all list (deposit , withdraw and display) saves in account not athors 



    /// When I stopped at Collection on the main, he turned on the yellow light and said:
    /// collection initialization can be simplified    

    /// </summary>



    public class Program
    {
        static void Main()
        {
            // Accounts
            var accounts = new List<Account>
            {
                new Account(),
                new Account("Larry"),
                new Account("Moe", 2000),
                new Account("Curly", 5000)
            };

            AccountUtil.Display(accounts);
            AccountUtil.Deposit(accounts, 1000);
            AccountUtil.Withdraw(accounts, 2000);

            // Savings
            var savAccounts = new List<Account>
            {
                new SavingAccount(),
                new SavingAccount("Superman"),
                new SavingAccount("Batman", 2000),
                new SavingAccount("Wonderwoman", 5000, 5.0)
            };

            AccountUtil.Display(savAccounts);
            AccountUtil.Deposit(savAccounts, 1000);
            AccountUtil.Withdraw(savAccounts, 2000);

            // Checking
            var checAccounts = new List<Account>
            {
                new CheckingAccount(),
                new CheckingAccount("Larry2"),
                new CheckingAccount("Moe2", 2000),
                new CheckingAccount("Curly2", 5000)
            };

            AccountUtil.Display(checAccounts);
            AccountUtil.Deposit(checAccounts, 1000);
            AccountUtil.Withdraw(checAccounts, 2000);
            AccountUtil.Withdraw(checAccounts, 2000);

            // Trust
            var trustAccounts = new List<Account>
            {
                new TrustAccount(),
                new TrustAccount("Superman2"),
                new TrustAccount("Batman2", 2000),
                new TrustAccount("Wonderwoman2", 5000, 5.0)
            };

            AccountUtil.Display(trustAccounts);
            AccountUtil.Deposit(trustAccounts, 1000);
            AccountUtil.Deposit(trustAccounts, 6000);
            AccountUtil.Withdraw(trustAccounts, 2000);
            AccountUtil.Withdraw(trustAccounts, 3000);
            AccountUtil.Withdraw(trustAccounts, 500);

            Console.WriteLine();
            Console.ReadLine();
        }
    }
}

