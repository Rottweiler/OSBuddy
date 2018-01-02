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

            Console.WriteLine(first.Name + ", is it members only? " + (first.MembersOnly ? " Yes." : "No."));
            Console.ReadLine();
        }
    }
}
