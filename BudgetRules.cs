// =====================================================================
//  BudgetRules.cs  —  the PROVIDED, TESTABLE CONTRACT for Assignment 1.
//
//  >>> THIS IS WHERE YOU WORK. <<<
//
//  The instructor-provided xUnit tests (ExpenseTracker.Tests) target this
//  class by name. Do NOT rename the class, the methods, or change their
//  signatures, and do NOT modify the tests. Replace each "throw new
//  NotImplementedException()" with a correct implementation so that every
//  test passes (Test Explorer, or:  dotnet test).
//
//  Work one method at a time. Run the tests, watch them turn green.
//  All methods are pure: no Console I/O, no shared state.
// =====================================================================
using static ExpenseTracker.MyBudget;

namespace ExpenseTracker;

public static class BudgetRules
{
    public const decimal MaxAmount = 1_000_000m;     // upper sanity limit
    public const decimal NearLimitFraction = 0.10m;  // "almost out" threshold




    /// <summary>
    /// Validates a monetary amount and returns it rounded to two decimal places.
    /// Throw <see cref="InvalidExpenseException"/> when the amount is not greater
    /// than zero, or is greater than <see cref="MaxAmount"/>.
    /// </summary>
    public static decimal ValidateAmount(decimal amount)
    {
        // TODO: guard clauses + decimal.Round(amount, 2)
        if (amount <= 0 || amount > MaxAmount)
        {
            throw new InvalidExpenseException($"Amount must be greater than zero and cannot exceed {MaxAmount}.");
        }
        return decimal.Round(amount, 2, MidpointRounding.AwayFromZero);

    }

    /// <summary>
    /// Classifies a positive amount into a size band: "Micro" (&lt; 10),
    /// "Small" (&lt; 50), "Medium" (&lt; 200), otherwise "Large".
    /// Throw <see cref="InvalidExpenseException"/> for a non-positive amount.
    /// Hint: use a switch expression with relational patterns.
    /// </summary>
    public static string ClassifyAmount(decimal amount)
    {
        // TODO
        switch (amount)
        {
            case <= 10:
                {
                    return "Micro"; break;
                }
            case <= 50:
                {
                    return "Small"; break;
                }
            case <= 200:
                {
                    return "Medium"; break;
                }
            default:
                {
                    return "Large"; break;
                }
        }
        throw new NotImplementedException();
    }

    /// <summary>
    /// Maps free-text input to one of the five canonical category names
    /// (Food, Transport, Utilities, Entertainment, Other), case-insensitively
    /// and allowing the first-letter shortcut. Return <c>null</c> when the
    /// input is not recognised. Hint: a switch expression over the trimmed,
    /// lower-cased input, with "food" or "f" => "Food", etc.
    /// </summary> 
    public static string? NormalizeCategory(string? input)
    {

        List<Category> Categories = Category.DefaultCategories;



        if (string.IsNullOrWhiteSpace(input)) return null;

        string cleanInput = input.Trim().ToLowerInvariant();

        // Use a switch expression to find the matching Category from your list
        Category? matchedCategory = cleanInput switch
        {
            "1" or "food" or "f"  =>  Categories.FirstOrDefault(c => c.CategoryId == 1),
            "2" or "transport" or "t"  => Categories.FirstOrDefault(c => c.CategoryId == 2),
            "3" or "utilities" or "u"  => Categories.FirstOrDefault(c => c.CategoryId == 3),
            "4" or "entertainment" or "e"  => Categories.FirstOrDefault(c => c.CategoryId == 4),
            "5" or "other" or "o"  => Categories.FirstOrDefault(c => c.CategoryId == 5),
             
            _ => null
        };
         
        return matchedCategory?.CategoryName;

    }

    /// <summary>
    /// Returns a budget-status message from the remaining funds and the limit:
    /// "OVER BUDGET" (remaining &lt; 0), "Almost out" (less than 10% of the
    /// limit remains), or "On track". Throw when the limit is not positive.
    /// </summary>
    public static string BudgetStatus(decimal remaining, decimal monthlyLimit)
    {

        // 1. Guard clause: Throw an exception if the limit is zero or negative
        if (monthlyLimit <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(monthlyLimit), "The monthly limit must be greater than zero.");
        }

        return remaining switch
        {
            < 0 => "OVER BUDGET",
            _ when remaining < monthlyLimit * 0.10m => "Almost out",
            _ => "On track"
        };

    }

    /// <summary>
    /// Formats an amount as currency using the default "$" symbol.
    /// Implement this as an expression-bodied member that calls the overload.
    /// </summary>
    public static string FormatCurrency(decimal amount)
    {

       return FormatCurrency(amount, "$");

        
    }

    /// <summary>
    /// Formats an amount as currency using the given symbol, e.g. "$62.40".
    /// </summary>
    public static string FormatCurrency(decimal amount, string currencySymbol)
    {
        //return $"{amount:C}";
        return $"{currencySymbol}{amount:0.00}";
        
    }
}

/// <summary>
/// Custom exception for invalid expense data (Module 4: custom exceptions).
/// This one is provided complete — use it from the methods above.
/// </summary>
public sealed class InvalidExpenseException(string message) : Exception(message);
