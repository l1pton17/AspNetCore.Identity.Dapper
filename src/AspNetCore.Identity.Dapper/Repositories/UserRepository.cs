using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;

namespace AspNetCore.Identity.Dapper.Repositories
{
    public class UserRepository<TUser, TKey, TUserClaim, TUserRole, TUserLogin, TUserToken> : RepositoryBase<TUser, TKey>
        where TUser : IdentityUser<TKey, TUserClaim, TUserRole, TUserLogin, TUserToken>
        where TKey : IEquatable<TKey>
    {
        public UserRepository(DbManager database)
            : base(database, "Users")
        {
        }

        public Task SetUserNameAsync(TUser user, string userName)
        {
            user.UserName = userName;

            return SetPropertyAsync(user.Id, nameof(IdentityUser.UserName), userName);
        }

        public Task SetNormalizedUserNameAsync(TUser user, string normalizedName)
        {
            user.NormalizedUserName = normalizedName;

            return SetPropertyAsync(user.Id, nameof(IdentityUser.NormalizedUserName), normalizedName);
        }

        public Task SetPasswordHashAsync(TUser user, string passwordHash)
        {
            user.PasswordHash = passwordHash;

            return SetPropertyAsync(user.Id, nameof(IdentityUser.PasswordHash), passwordHash);
        }

        public Task SetPhoneNumberAsync(TUser user, string phoneNumber)
        {
            user.PhoneNumber = phoneNumber;

            return SetPropertyAsync(user.Id, nameof(IdentityUser.PhoneNumber), phoneNumber);
        }

        public Task SetPhoneNumberConfirmedAsync(TUser user, bool confirmed)
        {
            user.PhoneNumberConfirmed = confirmed;

            return SetPropertyAsync(user.Id, nameof(IdentityUser.PhoneNumberConfirmed), confirmed);
        }

        public Task SetSecurityStamp(TUser user, string stamp)
        {
            user.SecurityStamp = stamp;

            return SetPropertyAsync(user.Id, nameof(IdentityUser.SecurityStamp), stamp);
        }

        public Task SetTwoFactorEnabledAsync(TUser user, bool enabled)
        {
            user.TwoFactorEnabled = enabled;

            return SetPropertyAsync(user.Id, nameof(IdentityUser.TwoFactorEnabled), enabled);
        }

        public Task SetEmailAsync(TUser user, string email)
        {
            user.Email = email;

            return SetPropertyAsync(user.Id, nameof(IdentityUser.Email), email);
        }

        public Task SetEmailConfirmedAsync(TUser user, bool confirmed)
        {
            user.EmailConfirmed = confirmed;

            return SetPropertyAsync(user.Id, nameof(IdentityUser.EmailConfirmed), confirmed);
        }

        public Task SetNormalizedEmailAsync(TUser user, string normalizedEmail)
        {
            user.NormalizedEmail = normalizedEmail;

            return SetPropertyAsync(user.Id, nameof(IdentityUser.NormalizedEmail), normalizedEmail);
        }

        public Task SetAccessFailedCountAsync(TUser user, int value)
        {
            user.AccessFailedCount = value;

            return SetPropertyAsync(user.Id, nameof(IdentityUser.AccessFailedCount), 0);
        }

        public Task SetLockoutEnabledAsync(TUser user, bool enabled)
        {
            user.LockoutEnabled = enabled;

            return SetPropertyAsync(user.Id, nameof(IdentityUser.LockoutEnabled), enabled);
        }

        public Task SetLockoutEndDateAsync(TUser user, DateTime? lockoutEndUtc)
        {
            user.LockoutEndUtc = lockoutEndUtc;

            return SetPropertyAsync(user.Id, nameof(IdentityUser.LockoutEndUtc), lockoutEndUtc);
        }

        public Task IncrementAccessFailedCountAsync(TKey id)
        {
            return DbManager.Connection.ExecuteAsync(
                $@"UPDATE {TableName}
                   SET AccessFailedCount = AccessFailedCount + 1
                   WHERE Id=@Id",
                new {Id = id});
        }

        public Task InsertAsync(TUser user)
        {
            return DbManager.Connection.InsertAsync(user);
        }

        public Task UpdateAsync(TUser user)
        {
            return DbManager.Connection.UpdateAsync(user);
        }

        public Task DeleteAsync(TUser user)
        {
            return DbManager.Connection.ExecuteAsync(
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
    }
}
