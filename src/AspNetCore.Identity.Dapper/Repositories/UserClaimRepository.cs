﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Dapper;
using Dapper.Contrib.Extensions;

namespace AspNetCore.Identity.Dapper.Repositories
{
    public class UserClaimRepository<TUser, TUserClaim, TKey> : RepositoryBase<TUserClaim, TKey>
        where TUserClaim : IdentityUserClaim<TKey>
        where TKey: IEquatable<TKey>
    {
        public UserClaimRepository(DbManager dbManager)
            : base(dbManager, "UserClaims")
        {
        }

        public Task InsertManyAsync(IEnumerable<TUserClaim> userClaims)
        {
            return DbManager.Connection.InsertAsync(userClaims);
        }

        public Task<IEnumerable<TUser>> FindUsersForClaimAsync(Claim claim)
        {
            var usersTableName = "Users";

            return DbManager.Connection.QueryAsync<TUser>(
                $@"SELECT *
                   FROM {usersTableName} users JOIN {TableName} userClaims
                        ON users.Id = userClaims.UserId
                   WHERE userClaims.ClaimValue = @ClaimValue AND userClaims.ClaimType = @ClaimType",
                new {ClaimValue = claim.Value, ClaimType = claim.Type});
        }

        public Task<IEnumerable<TUserClaim>> FindAsync(TKey userId)
        {
            return DbManager.Connection.QueryAsync<TUserClaim>(
                $"SELECT * FROM {TableName} WHERE UserId=@UserId",
                new {UserId = userId});
        }

        public Task UpdateAsync(TKey userId, Claim claim, Claim newClaim)
        {
            return DbManager.Connection.ExecuteAsync(
                $@"UPDATE {TableName}
                   SET ClaimValue=@NewClaimValue
                      ,ClaimType=@NewClaimType
                   WHERE ClaimValue=@OldClaimValue
                         AND ClaimType=@OldClaimType",
                new
                {
                    NewClaimValue = newClaim.Value,
                    NewClaimType = newClaim.Type,
                    OldClaimType = claim.Type,
                    OldClaimValue = claim.Value
                });
        }

        public Task DeleteAsync(TKey userId, IEnumerable<Claim> claims)
        {
            return DbManager.Connection.ExecuteAsync(
                $@"DELETE FROM {TableName}
                   WHERE UserId=@UserId AND ClaimValue=@ClaimValue AND ClaimType=@ClaimType",
                claims.Select(claim => new
                {
                    UserId = userId,
                    ClaimValue = claim.Value,
                    ClaimType = claim.Type
                }));
        }
    }
}