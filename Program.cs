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
                Console.WriteLine();
                Console.WriteLine("==============================================================");
                Console.WriteLine("              Welcome to Your Financial Dashboard  ");
                Console.WriteLine();

                // The following lines of code display the main menu options to the user.
                foreach (var menu in Menu.MainMenu)
                {
                    Console.WriteLine($"{menu.MenuId}) {menu.MenuName}   ");
                }
                Console.WriteLine("==============================================================");


                if (int.TryParse(Console.ReadLine(), out int menuNumber))
                {
                    if (menuNumber < 1 || menuNumber > Menu.MainMenu.Count)
                    {
                        Console.WriteLine("Menu ID must be greater than zero and less than or equal to the maximum menu ID. Please try again.");
                        Console.WriteLine();
                        continue;

                    }
                    else
                    {
                        switch (menuNumber)
                        {
                            case 1:
                                Console.WriteLine("You have selected: Add an expense"); 
                                break;
                            case 2:
                                Console.WriteLine("You have selected: Add an item");
                                break;
                            case 3:
                                Console.WriteLine("You have selected: Add a category");
                                break;
                            case 4:
                                Console.WriteLine("You have selected: View summary");
                                break;
                            case 5:
                                Console.WriteLine("You have selected: Set monthly budget");
                                break;
                            case 6:
                                Console.WriteLine("Exiting the application. Goodbye!");
                                menuContinue = false;
                                break;
                            default:
                                Console.WriteLine("Invalid menu selection. Please try again.");
                                break;
                        }



                        //Console.WriteLine($"You have selected: {selectedItem.Name} - {selectedItem.Price:C2} x {quantity}");
                    }

                    Console.WriteLine("-------------------------------------------------------------");
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


        
    }




    // The Budget class represents the monthly budget that users can set for their expenses.
    public class Budget
    {
        public int BudgetId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Month { get; set; }
        public Budget(int budgetId, decimal amount, DateTime month)
        {
            BudgetId = budgetId;
            Amount = amount;
            Month = month;
        }
    }

    // The Expense class represents the expenses that users can track.
    public class Expense
    {
        public int ExpenseId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public int CategoryId { get; set; }
        public string Note { get; set; }

        public string Classification => BudgetRules.ClassifyAmount(this.Amount);

        
        public Expense(int expenseId, decimal amount, DateTime date, int categoryId, string note = "")
        {
            ExpenseId = expenseId;
            Amount = amount;
            Date = date;
            CategoryId = categoryId;
            Note = note;
        }
    }
     
    // The Category class represents the different categories of expenses.
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public Category(int categoryId, string category)
        {
            CategoryId = categoryId;
            CategoryName = category;
        }

        // The DefaultCategories property is a static list of Category.  
        public static readonly List<Category> DefaultCategories = new List<Category>
        {
            new Category(1, "Food"),
            new Category(2, "Transport"),
            new Category(3, "Utilities"),
            new Category(4, "Entertainment"),
            new Category(5, "Other")
        };
    }

    // The Menu class represents the options available in the application's main menu. 
    public class Menu
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }

        public Menu(int menuId, string menu)
        {
            MenuId = menuId;
            MenuName = menu;
        }
         // The MainMenu property is a static list of Menu
        public static readonly List<Menu> MainMenu = new List<Menu>
            {
                new Menu(1, "Add an expense"),
                new Menu(2, "Add an item"),
                new Menu(3, "Add a category"),
                new Menu(4, "View summary"),
                new Menu(5, "Set monthly budget"),
                new Menu(6, "Exit")
            };
        }
}

