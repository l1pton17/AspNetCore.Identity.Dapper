using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;

namespace AspNetCore.Identity.Dapper.Repositories
{
    public class UserLoginRepository<TUserLogin, TKey>
        where TUserLogin : IdentityUserLogin<TKey>, new()
        where TKey : IEquatable<TKey>
    {
        private readonly DbManager _database;
        //TODO: remove in a refactoring stage
        public string TableName { get; } = "UserLogins";

        public UserLoginRepository(DbManager database)
        {
            _database = database;
        }

        public Task InsertAsync(TUserLogin userLogin)
        {
            return _database.Connection.InsertAsync(userLogin);
        }

        public Task<TUserLogin> FindByProviderOrDefaultAsync(string loginProvider, string providerKey)
        {
            return _database.Connection.QueryFirstOrDefaultAsync<TUserLogin>(
                $@"SELECT *
                   FROM {TableName}
                   WHERE LoginProvider=@LoginProvider AND ProviderKey=@ProviderKey",
                new {LoginProvider = loginProvider, ProviderKey = providerKey});
        }

        public Task<IEnumerable<TUserLogin>> FindByUserId(TKey userId)
        {
            return _database.Connection.QueryAsync<TUserLogin>(
                $@"SELECT *
                   FROM {TableName}
                   WHERE UserId=@UserId",
                new {UserId = userId});
        }

        public Task DeleteAsync(TKey userId, string loginProvider, string providerKey)
        {
            return _database.Connection.ExecuteAsync(
                $@"DELETE FROM {TableName}
                   WHERE UserId=@UserId
                         AND LoginProvider=@LoginProvider
                         AND ProviderKey=@ProviderKey",
                new {UserId = userId, LoginProvider = loginProvider, ProviderKey = providerKey});
        }
    }
}
