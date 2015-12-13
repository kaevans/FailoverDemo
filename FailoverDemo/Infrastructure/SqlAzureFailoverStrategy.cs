using System;
using System.Data.Entity.SqlServer;
using System.Data.SqlClient;

namespace FailoverDemo.Infrastructure
{
    public class SqlAzureFailoverStrategy : SqlAzureExecutionStrategy
    {
        private IFailoverProvider _provider;
        public SqlAzureFailoverStrategy(int maxRetryCount, TimeSpan maxDelay, IFailoverProvider provider) : base(maxRetryCount, maxDelay)
        {
            _provider = provider;
        }

        /// <summary>
        /// Gets the next TimeSpan to delay.  If null,
        /// then the operation will not be retried and
        /// a failover should be initiated.
        /// </summary>
        /// <param name="lastException">The exception that
        /// occurred</param>
        /// <returns>TimeSpan - null if no further retries
        /// will be attempted.</returns>
        protected override TimeSpan? GetNextDelay(Exception lastException)
        {
            TimeSpan? ret = base.GetNextDelay(lastException);
            if (!ret.HasValue)
            {
                _provider.ShouldFailover = true;
            }

            return ret;
        }

        /// <summary>
        /// Determines what errors are considered transient and
        /// should be retried.  SqlAzureExecutionStrategy will
        /// retry on 40613, 40501, 40197, 10929, 10928, 10060, 
        /// 10054, 10053, 233, 64 and 20.  This method adds
        /// 4060.  
        /// </summary>
        /// <param name="exception">The exception that occurred</param>
        /// <returns>Bool indicating if the operation should
        /// be retried</returns>
        protected override bool ShouldRetryOn(Exception exception)
        {
            var retry = base.ShouldRetryOn(exception);
            if (!retry)
            {
                SqlException oops = exception as SqlException;

                if (null != oops)
                {
                    switch (oops.Number)
                    {
                        //Server rejected the connection
                        case 4060:
                        case 18456:
                            retry = true;
                            break;
                    }
                }                
            }

            return retry;
        }
    }
}
