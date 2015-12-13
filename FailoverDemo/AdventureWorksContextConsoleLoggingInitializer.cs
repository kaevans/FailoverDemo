using System;
using System.Data.Entity;

namespace FailoverDemo
{
    /// <summary>
    /// By implementing an IDatabaseInitalizer, we are able
    /// to control the initialization routine.  Otherwise, 
    /// when we fail over to another connection string, we 
    /// would get an AutomaticMigrationsDisabledException.
    /// If we enabled automatic migrations, we would get
    /// SqlException indicating there is already a table
    /// 'Photos' in the database.
    /// </summary>
    public class AdventureWorksContextConsoleLoggingInitializer : IDatabaseInitializer<AdventureWorksContext>        
    {
        public void InitializeDatabase(AdventureWorksContext context)
        {
            context.Database.Log = Console.Write;
        }
    }
}
