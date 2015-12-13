using System;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;

namespace FailoverDemo.Infrastructure
{
    internal class ConnectionInterceptor : IDbConnectionInterceptor
    {
        public void Opening(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
        {
            //This is where we figure out which connection string to load
            CUIFailoverProvider provider = new CUIFailoverProvider();
            connection.ConnectionString = provider.GetConnectionString();
        }



        #region NotImplemented
        public void BeganTransaction(DbConnection connection, BeginTransactionInterceptionContext interceptionContext)
        {

        }

        public void BeginningTransaction(DbConnection connection, BeginTransactionInterceptionContext interceptionContext)
        {

        }

        public void Closed(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
        {

        }

        public void Closing(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
        {

        }

        public void ConnectionStringGetting(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
        {

        }

        public void ConnectionStringGot(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
        {

        }

        public void ConnectionStringSet(DbConnection connection, DbConnectionPropertyInterceptionContext<string> interceptionContext)
        {

        }

        public void ConnectionStringSetting(DbConnection connection, DbConnectionPropertyInterceptionContext<string> interceptionContext)
        {

        }

        public void ConnectionTimeoutGetting(DbConnection connection, DbConnectionInterceptionContext<int> interceptionContext)
        {

        }

        public void ConnectionTimeoutGot(DbConnection connection, DbConnectionInterceptionContext<int> interceptionContext)
        {

        }

        public void DatabaseGetting(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
        {

        }

        public void DatabaseGot(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
        {

        }

        public void DataSourceGetting(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
        {

        }

        public void DataSourceGot(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
        {

        }

        public void Disposed(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
        {

        }

        public void Disposing(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
        {

        }

        public void EnlistedTransaction(DbConnection connection, EnlistTransactionInterceptionContext interceptionContext)
        {

        }

        public void EnlistingTransaction(DbConnection connection, EnlistTransactionInterceptionContext interceptionContext)
        {

        }

        public void Opened(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
        {

        }



        public void ServerVersionGetting(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
        {

        }

        public void ServerVersionGot(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
        {

        }



        public void StateGetting(DbConnection connection, DbConnectionInterceptionContext<ConnectionState> interceptionContext)
        {
            
        }

        public void StateGot(DbConnection connection, DbConnectionInterceptionContext<ConnectionState> interceptionContext)
        {
            
        }

        #endregion
    }
}
