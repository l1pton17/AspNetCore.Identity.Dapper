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

        public Task SetUserNameAsync(TKey id, string userName)
        {
            return SetPropertyAsync(id, nameof(IdentityUser.UserName), userName);
        }

        public Task SetNormalizedUserNameAsync(TKey id, string normalizedName)
        {
            return SetPropertyAsync(id, nameof(IdentityUser.NormalizedUserName), normalizedName);
        }

        public Task SetPasswordHashAsync(TKey id, string passwordHash)
        {
            return SetPropertyAsync(id, nameof(IdentityUser.PasswordHash), passwordHash);
        }

        public Task SetPhoneNumberAsync(TKey id, string phoneNumber)
        {
            return SetPropertyAsync(id, nameof(IdentityUser.PhoneNumber), phoneNumber);
        }

        public Task SetPhoneNumberConfirmedAsync(TKey id, bool confirmed)
        {
            return SetPropertyAsync(id, nameof(IdentityUser.PhoneNumberConfirmed), confirmed);
        }

        public Task SetSecurityStamp(TKey id, string stamp)
        {
            return SetPropertyAsync(id, nameof(IdentityUser.SecurityStamp), stamp);
        }

        public Task SetTwoFactorEnabledAsync(TKey id, bool enabled)
        {
            return SetPropertyAsync(id, nameof(IdentityUser.TwoFactorEnabled), enabled);
        }

        public Task SetEmailAsync(TKey id, string email)
        {
            return SetPropertyAsync(id, nameof(IdentityUser.Email), email);
        }

        public Task SetEmailConfirmedAsync(TKey id, bool confirmed)
        {
            return SetPropertyAsync(id, nameof(IdentityUser.EmailConfirmed), confirmed);
        }

        public Task SetNormalizedEmailAsync(TKey id, string normalizedEmail)
        {
            return SetPropertyAsync(id, nameof(IdentityUser.NormalizedEmail), normalizedEmail);
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
            return FindByPropertyAsync(nameof(IdentityUser.Id), id);
        }

        public Task<TUser> FindByNameAsync(string normalizedUserName)
        {
            return FindByPropertyAsync(nameof(IdentityUser.NormalizedUserName), normalizedUserName);
        }

        public Task<TUser> FindByEmailAsync(string normalizedEmail)
        {
            return FindByPropertyAsync(nameof(IdentityUser.NormalizedEmail), normalizedEmail);
        }

        private Task<TUser> FindByPropertyAsync<T>(string propertyName, T propertyValue)
        {
            return _database.Connection.QueryFirstAsync<TUser>(
                $"SELECT * FROM {TableName} WHERE {propertyName}=@Value",
                new {Value = propertyValue});
        }

        private Task SetPropertyAsync<T>(TKey id, string propertyName, T propertyValue)
        {
            return _database.Connection.ExecuteAsync(
                $@"UPDATE {TableName} SET {propertyName}=@Value WHERE Id=@Id",
                new { Id = id, Value = propertyValue });
        }
    }
}
