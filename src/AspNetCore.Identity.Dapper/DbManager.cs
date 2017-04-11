using System;
using System.Data;
using System.Data.SqlClient;

namespace AspNetCore.Identity.Dapper
{
    public class DbManager : IDisposable
    {
        private readonly IDbConnection _connection;

        public IDbConnection Connection
        {
            get
            {
                if (_connection.State == ConnectionState.Closed)
                {
                    _connection.Open();
                }

                return _connection;
            }
        }

        public DbManager(string connectionString)
        {
            if (connectionString == null) throw new ArgumentNullException(nameof(connectionString));

            _connection = new SqlConnection(connectionString);
        }

        public void Dispose()
        {
            //TODO: maybe excess calling to close
            _connection.Close();
            _connection.Dispose();
        }
    }
}