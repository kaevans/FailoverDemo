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

        protected override TimeSpan? GetNextDelay(Exception lastException)
        {
            TimeSpan? ret = base.GetNextDelay(lastException);
            if (!ret.HasValue)
            {
                _provider.ShouldFailover = true;
            }

            return ret;
        }

        protected override bool ShouldRetryOn(Exception exception)
        {
            var retry = base.ShouldRetryOn(exception);
            if (!retry)
            {
                SqlException oops = exception as SqlException;
                if (null != oops && oops.Number == 4060)
                {
                    retry = true;
                }
            }

            return retry;
        }
    }
}
