using System;
using System.Data;
using AspNetCore.Identity.Dapper.Repositories;
using Microsoft.Extensions.Options;
using Npgsql;

namespace AspNetCore.Identity.Dapper.PostgreSql.Repositories
{
    public class NpgsqlConnectionFactory : IConnectionFactory
    {
        private readonly ConnectionSettings _connectionSettings;

        public NpgsqlConnectionFactory(IOptions<ConnectionSettings> connectionSettings)
        {
            _connectionSettings = connectionSettings.Value;
        }

        public IDbConnection Create()
        {
            var connectionString = _connectionSettings?.ConnectionString;

            if (String.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(_connectionSettings.ConnectionString));
            }

            return new NpgsqlConnection(connectionString);
        }
    }
}