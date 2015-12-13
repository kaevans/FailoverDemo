using FailoverDemo.Infrastructure;
using System;
using System.Data.Entity;

namespace FailoverDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //See comments for why this is necessary
            Database.SetInitializer(new AdventureWorksContextConsoleLoggingInitializer());
            do
            {
                try
                {
                    var context = new AdventureWorksContext();

                    foreach (var item in context.Customers)
                    {
                        Console.WriteLine(item.CustomerID);
                    }
                }
                catch(Exception oops)
                {
                    Console.WriteLine(oops.GetType() + " - " + oops.Message);
                }
            }
            while (true);

        }
    }
}
