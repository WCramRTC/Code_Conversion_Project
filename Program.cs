using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_Conversion_Project
{
    internal class Program
    {
        // Global Variables
        // Track Users Name
        static string userName;

        // Parallel arrays representing users accounts
        static string[] accountNames = { "Savings", "Checking"};
        static double[] balances = { 200000, 100000 };

        // Represents the index of the users account
        const int SavingsAccount = 0;
        const int CheckingAccount = 1;

        static void Main(string[] args)
        {

            Menu();

            Console.ReadKey();
        }

        // Full Menu
        public static void Menu()
        {
            // Controls if menu is running
            bool isRunning = true;

            // Ask user for name
            Console.Write("Enter your name: ");
            userName = Console.ReadLine();

            while (isRunning)
            {
                        
                Console.WriteLine("Welcome " + userName +". What would you like to do?");
                Console.WriteLine("1 - Display Account Information.");
                Console.WriteLine("2 - Deposit Money into an account.");
                Console.WriteLine("3 - Withdraw Money from an account.");
                Console.WriteLine("4 - Calculate how many moneys until debt is paid with interest.");
                Console.WriteLine("5 - Exit");

                Console.Write("Enter your choice: ");
                string userChoice = Console.ReadLine();

                if(userChoice == "1")
                {
                    DisplayAccounts();
                }
                else if(userChoice == "2")
                {
                    Deposit();
                }
                else if (userChoice == "3")
                {
                    Withdraw();
                }
                else if (userChoice == "4")
                {
                    DebtCalculator();
                }
                else if (userChoice == "5")
                {
                    isRunning = false;
                    Console.WriteLine("Good bye");
                }
                else
                {

                }


            }
            
        } // Menu()

        public static void DisplayAccounts()
        {
            for (int i = 0; i < accountNames.Length; i++)
            {
                Console.WriteLine($"{accountNames[i]} - {balances[i]}");
            }
        }

        // Deposit Money Method
        public static void Deposit()
        {
            // Let user choose with account to deposit money into
            Console.WriteLine("Enter account to deposit money.");
            Console.WriteLine("1 - Savings");
            Console.WriteLine("2 - Checking");
            Console.Write("Enter your choice: ");
            string userChoice = Console.ReadLine();

            // Set the index for the users chosen account, using our Constants
            // Set as -1 incase invalid input entered
            int index = -1;

            if (userChoice == "1")
            {
                index = SavingsAccount;
            }
            else if (userChoice == "2")
            {
                index = CheckingAccount;
            }
            else
            {
                Console.WriteLine("Invalid input, going back to the main menu.");
            }


            // Check that one of the accounts was chosen
            if(index == SavingsAccount || index == CheckingAccount)
            {
                // Ask for amount to deposit
                Console.WriteLine("How much would you like to deposit?");
                string userAmount = Console.ReadLine();
                // Parse amount
                double amount = double.Parse(userAmount);

                // Add amount to selected account and display new balance
                balances[index] += amount;
                Console.WriteLine($"Amount Deposited. New Balance {balances[index]}");
            }

        } // Deposit()

        // Withdraw
        public static void Withdraw()
        {
            Console.WriteLine("Enter account to withdraw money from.");
            Console.WriteLine("1 - Savings");
            Console.WriteLine("2 - Checking");
            Console.Write("Enter your choice: ");
            string userChoice = Console.ReadLine();

            // Set as -1 incase invalid input entered
            int index = -1;

            if (userChoice == "1")
            {
                index = SavingsAccount;
            }
            else if (userChoice == "2")
            {
                index = CheckingAccount;
            }
            else
            {
                Console.WriteLine("Invalid input, going back to the main menu.");
            }

            if (index == 0 || index == 1)
            {
                Console.WriteLine("How much would you like to withdraw?");
                string userAmount = Console.ReadLine();
                double amount = double.Parse(userAmount);

                // Check available balance to see if amount can be withdrawn
                // Only allow if there is enough money to withdrawn
                if (balances[index] > amount)
                {
                    balances[index] -= amount;
                    Console.WriteLine($"Amount Withdrawn. New Balance {balances[index]}");
                }
                else
                {
                    // Display message if not enough funds
                    Console.WriteLine("User funds not available.");
                }
                
            }
        } // Withdraw()

        public static void DebtCalculator()
        {
            // Ask user for current debt, interest rate and monthly payment
            Console.WriteLine("Enter your current debt.");
            string userDebit = Console.ReadLine();
            Console.WriteLine("Enter current interest rate. (  Ex 7.54 = 7.54%");
            string userInterest = Console.ReadLine();

            // Parse to numbers
            double debit = double.Parse(userDebit);
            // Translate user value into numerical equivilant ( Ex 7 turns into 1.07 for 
            double interest = double.Parse(userInterest) / 100.0;

            // calculate annual interest, then divide by 12 to get monthly increase
            double currentInterest = debit * (interest) / 12;


            Console.WriteLine("Enter monthly payment. Should be over " + currentInterest );
            string userMonthlyPayment = Console.ReadLine();


            double monthlyPayment = double.Parse(userMonthlyPayment);
            Console.WriteLine(interest);

            // Keep track of how many months it will take
            int totalMonth = 0;

            // While we still have an outstanding debit, loop
            while(debit > 0)
            {
                // Calculates how many years
                int year = totalMonth / 12;
                // Calculates how many months ( in base 12 )
                int month = totalMonth % 12;
                // Currently monthy interest
                currentInterest = debit * ( interest) / 12;

                // Remove monthly payment
                debit -= monthlyPayment;
                // Add monthly interest
                debit += currentInterest;
                
                // Display information
                Console.WriteLine($"Year {year} - Month {month} - Remaining Debit {debit.ToString("c")} - Monthly Interest {currentInterest.ToString("c")}");

                // Increament monthly counter
                totalMonth++;

                // If amount of time needed to pay off debt surpasses 100 years, stop the loop
                if (year > 100) break;
            }


        } // DebtCalculator


    } // class
} // namespace
