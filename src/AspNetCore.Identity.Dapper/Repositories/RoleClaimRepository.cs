using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;

namespace AspNetCore.Identity.Dapper.Repositories
{
    public class RoleClaimRepository<TRoleClaim, TKey> : RepositoryBase<TRoleClaim, TKey>
        where TRoleClaim : IdentityRoleClaim<TKey>
        where TKey : IEquatable<TKey>
    {
        public RoleClaimRepository(IDapperContext context)
            : base(context, context.RoleClaimsTableName)
        {
        }

        public Task<IEnumerable<TRoleClaim>> FindByRoleIdAsync(TKey roleId)
        {
            return Context.Connection.QueryAsync<TRoleClaim>(
                $"SELECT * FROM {TableName} WHERE RoleId=@RoleId",
                new {RoleId = roleId});
        }

        public Task InsertAsync(TRoleClaim roleClaim)
        {
            return Context.Connection.InsertAsync(roleClaim);
        }

        public Task DeleteClaimAsync(TKey roleId, Claim claim)
        {
            return Context.Connection.ExecuteAsync(
                $@"DELETE FROM {TableName}
                   WHERE RoleId=@RoleId AND ClaimValue=@ClaimValue AND ClaimType=@ClaimType",
                new {RoleId = roleId, ClaimValue = claim.Value, ClaimType = claim.Type});
        }
    }
}