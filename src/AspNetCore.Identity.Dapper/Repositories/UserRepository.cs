using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;

namespace AspNetCore.Identity.Dapper.Repositories
{
    public class UserRepository<TUser, TKey, TUserClaim, TUserRole, TUserLogin, TUserToken>
        where TUser : IdentityUser<TKey, TUserClaim, TUserRole, TUserLogin, TUserToken>
        where TKey : IEquatable<TKey>
    {
        private readonly DbManager _database;
        //TODO: remove in a refactoring stage
        public string TableName { get; } = "Users";

        public UserRepository(DbManager database)
        {
            _database = database;
        }

        public Task SetUserNameAsync(TUser user, string userName)
        {
            return _database.Connection.ExecuteAsync(
                $@"UPDATE {TableName} SET UserName=@UserName WHERE Id=@Id",
                new {Id = user.Id, UserName = userName});
        }

        public Task SetNormalizedUserNameAsync(TUser user, string normalizedName)
        {
            return _database.Connection.ExecuteAsync(
                $@"UPDATE {TableName} SET NormalizedUserName=@NormalizedName WHERE Id=@Id",
                new { Id = user.Id, NormalizedName = normalizedName });
        }

        public Task InsertAsync(TUser user)
        {
            return _database.Connection.InsertAsync(user);
        }

        public Task UpdateAsync(TUser user)
        {
            return _database.Connection.UpdateAsync(user);
        }

        public Task DeleteAsync(TUser user)
        {
            return _database.Connection.ExecuteAsync(
                $"DELETE FROM {TableName} WHERE Id=@Id",
                new {Id = user.Id});
        }

        public Task<TUser> FindByIdAsync(TKey id)
        {
            return _database.Connection.QueryFirstAsync<TUser>(
                $"SELECT * FROM {TableName} WHERE Id=@Id",
                new {Id = id});
        }

        public Task<TUser> FindByNameAsync(string normalizedUserName)
        {
            return _database.Connection.QueryFirstAsync<TUser>(
                $"SELECT * FROM {TableName} WHERE NormalizedUserName=@NormalizedUserName",
                new {NormalizedUserName = normalizedUserName});
        }
    }
}
