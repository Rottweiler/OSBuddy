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
            var results = catalog.Search("red partyhat");

            var i = 0;
            foreach(var item in results)
            {
                i++;
                if(item.Tradeable)
                {
                    Console.WriteLine(item.Name + " Value: " + Exchange.GetValue(item));
                }
            }

            Console.ReadLine();
        }
    }
}
```
