using FailoverDemo.Infrastructure;
using System;
using System.Data.Entity;

namespace FailoverDemo
{
    public class AdventureWorksContextConfiguration : DbConfiguration
    {
        public AdventureWorksContextConfiguration() : base()
        {
            CUIFailoverProvider provider = new CUIFailoverProvider();
            //Set the failover execution strategy to handle 
            //transient faults and to tell us when we 
            //need to failover
            this.SetExecutionStrategy("System.Data.SqlClient", () =>
                new SqlAzureFailoverStrategy(3, new TimeSpan(0, 0, 10), provider));
            
            //Add the connection interceptor so we can change
            //the connection string when failover occurs
            this.AddInterceptor(new ConnectionInterceptor(provider));

        }
    }
}
