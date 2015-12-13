using FailoverDemo.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FailoverDemo
{
    public class PhotoContextConfiguration : DbConfiguration
    {
        public PhotoContextConfiguration() : base()
        {
            //Set the failover execution strategy to handle 
            //transient faults and to tell us when we 
            //need to failover
            this.SetExecutionStrategy("System.Data.SqlClient", () =>
                new SqlAzureFailoverStrategy(3, new TimeSpan(0, 0, 5), new CUIFailoverProvider()));
            
            //Add the connection interceptor so we can change
            //the connection string when failover occurs
            this.AddInterceptor(new ConnectionInterceptor());

        }
    }
}
