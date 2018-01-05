# OSBuddy
OSBuddy API written in C# .NET to pull and analyze items and Grand Exchange data from RuneScape. Includes features that will help display the data in a fashionable manner. Usable with most .NET languages.

## Example usage
```csharp
using OSBuddy.RuneScape.OldSchool;
using System;
using System.Linq;

namespace OSBuddy.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ItemCatalogFactory();
            var catalog = factory.GetItemCatalog();
            var result = catalog.Search("red partyhat");
            var first = result.FirstOrDefault();
            var value = Exchange.GetValue(first);

            Console.WriteLine(first.Name + ", is it members only? " + (first.MembersOnly ? " Yes." : "No."));
            Console.WriteLine("Current value: " + Exchange.GetFormattedCurrency(value, false));
            Console.ReadLine();
        }
    }
}
```
