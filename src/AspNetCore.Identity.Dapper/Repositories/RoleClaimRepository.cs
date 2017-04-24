using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;

namespace AspNetCore.Identity.Dapper.Repositories
{
    public class RoleClaimRepository<TRoleClaim, TKey>
        where TRoleClaim : IdentityRoleClaim<TKey>
        where TKey : IEquatable<TKey>
    {
        private readonly DbManager _database;
        //TODO: remove in a refactoring stage
        public string TableName { get; } = "RoleClaims";

        public RoleClaimRepository(DbManager database)
        {
            _database = database;
        }

        public Task<IEnumerable<TRoleClaim>> FindByRoleIdAsync(TKey roleId)
        {
            return _database.Connection.QueryAsync<TRoleClaim>(
                $"SELECT * FROM {TableName} WHERE RoleId=@RoleId",
                new {RoleId = roleId});
        }

        public Task InsertAsync(TRoleClaim roleClaim)
        {
            return _database.Connection.InsertAsync(roleClaim);
        }

        public Task RemoveClaimAsync(TKey roleId, Claim claim)
        {
            return _database.Connection.ExecuteAsync(
                $@"DELETE FROM {TableName}
                   WHERE RoleId=@RoleId AND ClaimValue=@ClaimValue AND ClaimType=@ClaimType",
                new {RoleId = roleId, ClaimValue = claim.Value, ClaimType = claim.Type});
        }
    }
}