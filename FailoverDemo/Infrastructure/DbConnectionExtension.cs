using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FailoverDemo.Infrastructure
{
    public static class DbConnectionExtension
    {
        /// <summary>
        /// Wrapper method to determine if a connection
        /// is opened to a read only database.
        /// </summary>
        /// <param name="connection">The open connection</param>
        /// <returns>Bool indicating if the database is read only</returns>
        public static bool IsReadOnly(this System.Data.Common.DbConnection connection)
        {
            bool ret = false;
            IsDatabaseReadOnly(connection).ContinueWith(t => ret = t.Result);
            return ret;                
        }

        /// <summary>
        /// Determines if a database is in read only mode.
        /// </summary>
        /// <param name="connection">The database connection</param>
        /// <returns>True if readonly, otherwise false.</returns>
        private static async Task<bool> IsDatabaseReadOnly(System.Data.Common.DbConnection connection)
        {
            bool isReadOnly = false;
            DbDataReader reader = null;

            var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT is_read_only FROM sys.databases WHERE name = @databaseName";
            cmd.CommandType = CommandType.Text;
            var p = cmd.CreateParameter();
            p.ParameterName = "@databaseName";
            p.Value = connection.Database;
            //p.Value = "FailoverTest";
            cmd.Parameters.Add(p);
            try
            {
                reader = await cmd.ExecuteReaderAsync();
                if(!reader.IsClosed && reader.HasRows)
                {
                    await reader.ReadAsync();
                    isReadOnly = reader.GetBoolean(0);
                    
                }
            }
            catch (Exception oops)
            {
                System.Diagnostics.Debug.WriteLine(oops.Message);
            }
            finally
            {
                if(null != reader)
                    reader.Close();
            }

            

            return isReadOnly;        
        }
    }
}
