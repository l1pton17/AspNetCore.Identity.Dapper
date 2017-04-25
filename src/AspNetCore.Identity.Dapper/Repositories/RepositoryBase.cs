using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace AspNetCore.Identity.Dapper.Repositories
{
    public abstract class RepositoryBase<TEntity, TKey>
    {
        private readonly IConnectionFactory _connectionFactory;

        protected ITableConfiguration Configuration { get; }
        public IDbConnection Connection => _connectionFactory.Create();
        public string TableName { get; }

        protected RepositoryBase(
            IConnectionFactory connectionFactory,
            ITableConfiguration configuration,
            string tableName)
        {
            _connectionFactory = connectionFactory;
            TableName = tableName;
            Configuration = configuration;
        }

        protected virtual Task<TEntity> FindByPropertyAsync<T>(string propertyName, T propertyValue)
        {
            return Configuration.Connection.QueryFirstAsync<TEntity>(
                $"SELECT * FROM {TableName} WHERE {propertyName}=@Value",
                new { Value = propertyValue });
        }

        protected virtual Task SetPropertyAsync<T>(TKey id, string propertyName, T propertyValue)
        {
            return Configuration.Connection.ExecuteAsync(
                $@"UPDATE {TableName} SET {propertyName}=@Value WHERE Id=@Id",
                new { Id = id, Value = propertyValue });
        }
    }
}
