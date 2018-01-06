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
