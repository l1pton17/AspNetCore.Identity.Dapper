using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace AspNetCore.Identity.Dapper.Repositories
{
    public abstract class RepositoryBase<TEntity, TKey>
    {
        protected DbManager DbManager { get; }
        public string TableName { get; }

        protected RepositoryBase(DbManager dbManager, string tableName)
        {
            TableName = tableName;
            DbManager = dbManager;
        }

        protected virtual Task<TEntity> FindByPropertyAsync<T>(string propertyName, T propertyValue)
        {
            return DbManager.Connection.QueryFirstAsync<TEntity>(
                $"SELECT * FROM {TableName} WHERE {propertyName}=@Value",
                new { Value = propertyValue });
        }

        protected virtual Task SetPropertyAsync<T>(TKey id, string propertyName, T propertyValue)
        {
            return DbManager.Connection.ExecuteAsync(
                $@"UPDATE {TableName} SET {propertyName}=@Value WHERE Id=@Id",
                new { Id = id, Value = propertyValue });
        }
    }
}
