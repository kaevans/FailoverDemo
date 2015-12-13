using System;
using System.Data.Entity;

namespace FailoverDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //See comments for why this is necessary
            Database.SetInitializer(new PhotoContextConsoleLoggingInitializer());

            PhotoContext context = new PhotoContext();
            foreach (var item in context.Photos)
            {
                Console.WriteLine(item.PhotoId);
            }
        }
    }
}
