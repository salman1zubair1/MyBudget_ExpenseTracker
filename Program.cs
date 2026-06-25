//Full Name - Mohammed Zubair Mohammed Salman
//Student Id - N01795248
//Humber Polytechnic - Software Development Bridging Program Back-End
//Assignment 2 


// =====================================================================
//  Program.cs  —  the interactive console UI for MyBudget (Assignment 1).
//  Target framework: .NET 10 (LTS), language C# 14.
//
//  >>> BUILD THE MENU-DRIVEN UI HERE (Modules 1-3). <<<
//
//  Once you have implemented BudgetRules.cs (so the unit tests pass), wire it
//  up to a console interface that meets the assignment brief:
//
//    * Print a banner (try a raw string literal).
//    * Loop a menu until the user exits, using a switch on the choice:
//        1) Add an expense   2) View summary   3) Set monthly budget   4) Exit
//    * Read and VALIDATE input, re-prompting on bad data (decimal.TryParse,
//      BudgetRules.NormalizeCategory, a date parse, non-empty text).
//    * Keep running totals in simple variables (no collections / no classes).
//    * Use BudgetRules.ValidateAmount / ClassifyAmount / BudgetStatus /
//      FormatCurrency for all logic and formatting.
//    * Handle bad input with try / catch / finally and InvalidExpenseException.
//
//  See section 6 of the assignment brief for a sample run to aim for.
// =====================================================================

namespace ExpenseTracker
{
    public class MyBudget
    {
        // Overall running totals
        public static decimal totalExpensesAmount { get; set; } = 0m;
        public static int expenseCount { get; set; } = 0;
        public static decimal highestSingleExpense { get; set; } = 0m;

        // Category-specific running totals
        public static decimal totalFood { get; set; } = 0m;
        public static decimal totalTransport { get; set; } = 0m;
        public static decimal totalUtilities { get; set; } = 0m;
        public static decimal totalEntertainment { get; set; } = 0m;
        public static decimal totalOther { get; set; } = 0m;

        // Monthly budget limit (default to 1000)
        public static decimal monthlyLimit { get; set; } = 0m;

        static void Main(string[] args)
        {
            // The following lines of code display the welcome message and the title of the application.
            Console.WriteLine("");
            Console.WriteLine("==============================================================");
            Console.WriteLine("||__________________________________________________________||");
            Console.WriteLine("||                                                          ||");
            Console.WriteLine("||                        MY BUDGET                         ||");
            Console.WriteLine("||                  --------------------                    ||");
            Console.WriteLine("||                     EXPENSE TRACKER                      ||");
            Console.WriteLine("||                  --------------------                    ||");
            Console.WriteLine("==============================================================");
            bool menuContinue = true;
            while (menuContinue)
            {
                // The following lines of code display the welcome message
                Console.WriteLine();
                //Console.WriteLine();
                //Console.WriteLine("==============================================================");
                Console.WriteLine("              Welcome to Your Financial Dashboard  ");
                Console.WriteLine();

                // The following lines of code display the main menu options to the user.
                Console.WriteLine("1. Add an expense");
                Console.WriteLine("2. View summary");
                Console.WriteLine("3. Set monthly budget");
                Console.WriteLine("4. Exit");

                Console.WriteLine("==============================================================");

                // The following lines of code prompt the user to select a menu option and validate the input.
                if (int.TryParse(Console.ReadLine(), out int menuNumber))
                {
                    if (menuNumber < 1 || menuNumber > 5)
                    {
                        Console.WriteLine("Menu ID must be greater than zero and less than or equal to four. Please try again.");
                        Console.WriteLine();
                        continue;

                    }
                    else
                    {
                        // The following switch statement handles the user's menu selection and calls the appropriate methods based on the selected option.
                        switch (menuNumber)
                        {
                            case 1:
                                Console.WriteLine("You have selected: Add an expense");
                                AddanExpense();
                                break;
                            case 2:
                                Console.WriteLine("You have selected: View summary");
                                Viewsummary();
                                break;
                            case 3:
                                Console.WriteLine("You have selected: Set monthly budget");
                                Setmonthlybudget();
                                break;
                            case 4:
                                Console.WriteLine("Exiting the application.");
                                Console.WriteLine("Thank you for using MyBudget. Goodbye!");
                                Console.WriteLine("-------------------------------------------------------------");
                                menuContinue = false;
                                break;
                            default:
                                Console.WriteLine("Invalid menu selection. Please try again.");
                                break;
                        }
                    }
                    //Console.WriteLine("-------------------------------------------------------------");
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid menu number.");
                    Console.WriteLine();
                }

                Console.WriteLine();
                Console.WriteLine("-------------------------------------------------------------");
                Console.WriteLine();
                Console.WriteLine();
            }
        }

        // The following method handles the process of adding an expense, including input validation and updating totals.
        public static void AddanExpense()
        {
            bool expenseAdded = true;
            while (expenseAdded)
            {
                string categoryInput = "";
                decimal amountInput = 0m;
                DateOnly DateOnlyInput = DateOnly.FromDateTime(DateTime.Now);
                string noteInput = "";

                Console.WriteLine();
                Console.WriteLine("-------------------------------------------------------------");
                Console.WriteLine("Please select a category from the list below:");
                // The following lines of code display the category options to the user.
                Console.WriteLine("1. Food");
                Console.WriteLine("2. Transport");
                Console.WriteLine("3. Utilities");
                Console.WriteLine("4. Entertainment");
                Console.WriteLine("5. Other");

                Console.WriteLine("-------------------------------------------------------------");
                Console.WriteLine("Please enter the category ID, category name or category first letter shortcut (f, t, u, e, o):");

                categoryInput = BudgetRules.NormalizeCategory(Console.ReadLine());
                if (categoryInput == null)
                {
                    Console.WriteLine("Invalid category input. Please try again.");
                    Console.WriteLine();
                    continue;
                }


                while (true)
                {
                    Console.WriteLine($"You have selected: {categoryInput}");


                    Console.Write("Please enter the expense amount (e.g., 123.45):");
                    string input = Console.ReadLine();
                    if (decimal.TryParse(input, out amountInput))
                    {
                        try
                        {
                            // Validate the amount and classify it
                            decimal validatedAmount = BudgetRules.ValidateAmount(amountInput);
                            string classification = BudgetRules.ClassifyAmount(validatedAmount);
                            Console.WriteLine($"Expense amount: {BudgetRules.FormatCurrency(validatedAmount)} - Classification: {classification}");

                            // Update category totals and overall summary
                            switch (categoryInput)
                            {
                                case "Food": totalFood += validatedAmount; break;
                                case "Transport": totalTransport += validatedAmount; break;
                                case "Utilities": totalUtilities += validatedAmount; break;
                                case "Entertainment": totalEntertainment += validatedAmount; break;
                                case "Other": totalOther += validatedAmount; break;
                            }
                            totalExpensesAmount += validatedAmount;
                            expenseCount += 1;
                            if (validatedAmount > highestSingleExpense)
                                highestSingleExpense = validatedAmount;
                            if (monthlyLimit is not 0)
                            {
                                if (totalExpensesAmount > monthlyLimit)
                                    Console.WriteLine("Warning: You have exceeded your monthly budget limit!");
                            }

                            break; // Exit the loop if the amount is valid
                        }
                        catch (InvalidExpenseException ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}. Please try again.");
                            Console.WriteLine();
                            continue; // Re-enter for valid input                            
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid decimal number for the expense amount.");
                        Console.WriteLine();
                        continue;
                    }
                }

                while (true)
                {

                    Console.WriteLine("Enter yyyy-MM-dd for past / today dates. Do not enter future dates. Leave blank for today");

                    string inputDate = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(inputDate))
                    {
                        DateOnlyInput = DateOnly.FromDateTime(DateTime.Now);
                        Console.WriteLine("Expense date recorded: " + DateOnlyInput.ToString("yyyy-MM-dd"));
                        break;// Exit the loop if the date is valid
                    }
                    else if (DateOnly.TryParse(inputDate, out DateOnly outDate) && (outDate <= DateOnly.FromDateTime(DateTime.Now)))
                    {
                        DateOnlyInput = outDate;
                        Console.WriteLine("Expense date recorded: " + DateOnlyInput.ToString("yyyy-MM-dd"));
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid date.");
                        Console.WriteLine();
                        continue;// Re-enter for valid input
                    }
                }

                while (true)
                {
                    Console.WriteLine("Please enter a note for the expense (optional, max 100 characters):");
                    noteInput = Console.ReadLine();// Read the note input from the user


                    if (noteInput.Length > 100)
                    {
                        Console.WriteLine("Description is too long. Please limit to 100 characters.");
                        Console.WriteLine();
                        continue;// Re-enter for valid input
                    }
                    else
                    {
                        Console.WriteLine($"Expense description recorded: {noteInput}");
                        break; // Exit the loop if the description is valid
                    }
                }

                // Display the expense details to the user
                Console.WriteLine("-------------------------------------------------------------");
                Console.WriteLine("Expense added successfully!");
                Console.WriteLine();
                Console.WriteLine("You have added the following expense now:");
                Console.WriteLine("-------------------------------------------------------------");
                Console.WriteLine($"Category: {categoryInput}");
                Console.WriteLine($"Amount: {BudgetRules.FormatCurrency(BudgetRules.ValidateAmount(amountInput))} - Classification: {BudgetRules.ClassifyAmount(BudgetRules.ValidateAmount(amountInput))}");
                Console.WriteLine($"Date: {DateOnlyInput.ToString("yyyy-MM-dd")}");
                if (!string.IsNullOrWhiteSpace(noteInput))
                {
                    Console.WriteLine($"Note: {noteInput}");
                }
                Console.WriteLine("-------------------------------------------------------------");
                Console.WriteLine();
                while (true)
                {
                    Console.WriteLine("Would you like to add another expense? (y/n):");
                    string continueInput = Console.ReadLine().Trim().ToLower();
                    if (continueInput == "y")
                    {
                        break; // Exit the inner loop to add another expense
                    }
                    else if (continueInput == "n")
                    {
                        expenseAdded = false; // Exit the outer loop to return to the main menu
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter 'y' for yes or 'n' for no.");
                        Console.WriteLine();
                        continue; // Re-enter for valid input 
                    }
                }
            }



        }

        // The following method show the summery
        public static void Viewsummary()
        {

            Console.WriteLine();
            Console.WriteLine("============================================================");
            Console.WriteLine("                          MY BUDGET                         ");
            Console.WriteLine("                       EXPENSE SUMMARY                      ");
            Console.WriteLine("============================================================");
            if (expenseCount > 0)
            {
                Console.WriteLine($"Total Count of Expenses: \t{expenseCount}");
                Console.WriteLine($"Total Overall Spending:  \t{BudgetRules.FormatCurrency(totalExpensesAmount)}");
                if (monthlyLimit is not 0)
                {
                    Console.WriteLine($"Monthly Budget: \t\t{BudgetRules.FormatCurrency(monthlyLimit)}");
                    if (totalExpensesAmount > monthlyLimit)
                    {
                        Console.WriteLine("Total remaining: \t\tYour monthly expenses have exceeded your budget limit!");
                    }
                    else
                    {
                        Console.WriteLine($"Total remaining: \t\t{BudgetRules.FormatCurrency(monthlyLimit - totalExpensesAmount)}");
                    }
                    Console.WriteLine($"Budget Status: \t\t\t{BudgetRules.BudgetStatus((monthlyLimit - totalExpensesAmount), monthlyLimit)}");
                }
                Console.WriteLine($"Average: \t\t\t{BudgetRules.FormatCurrency(totalExpensesAmount / expenseCount)}");
                Console.WriteLine($"Highest Single Expense: \t{BudgetRules.FormatCurrency(highestSingleExpense)}");

                Console.WriteLine();
                Console.WriteLine("-------------------------------------------------------------");
                Console.WriteLine("                      CATEGORY BREAKDOWN                     ");
                Console.WriteLine("-------------------------------------------------------------");
                Console.WriteLine($"Food: \t\t\t{BudgetRules.FormatCurrency(totalFood)}");
                Console.WriteLine($"Transport:\t\t{BudgetRules.FormatCurrency(totalTransport)}");
                Console.WriteLine($"Utilities:\t\t{BudgetRules.FormatCurrency(totalUtilities)}");
                Console.WriteLine($"Entertainment:\t\t{BudgetRules.FormatCurrency(totalEntertainment)}");
                Console.WriteLine($"Other: \t\t\t{BudgetRules.FormatCurrency(totalOther)}");
            }
            else
                Console.WriteLine("No expenses recorded yet."); Console.WriteLine();
            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine("                          THANK YOU!                         ");
            Console.WriteLine("=============================================================");
        }

        // The following method set monthly budget
        public static void Setmonthlybudget()
        {

            while (true)
            {
                try
                {
                    Console.WriteLine();
                    Console.Write("Enter your budget limit: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal inputLimit))
                    {
                        monthlyLimit = inputLimit;
                        Console.WriteLine($"Monthly budget set to: \t{BudgetRules.FormatCurrency(monthlyLimit)}");
                        Console.WriteLine($"Budget Status: \t\t{BudgetRules.BudgetStatus((monthlyLimit - totalExpensesAmount), monthlyLimit)}");
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }


}



