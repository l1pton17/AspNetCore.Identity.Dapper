using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using Dapper.Contrib.Extensions;

namespace AspNetCore.Identity.Dapper.Repositories
{
        where TUser : IdentityUser<TKey, TUserClaim, TUserRole, TUserLogin>
        where TRole : IdentityRole<TKey, TUserRole, TRoleClaim>
        where TKey : IEquatable<TKey>
        where TUserClaim : IdentityUserClaim<TKey>
        where TUserRole : IdentityUserRole<TKey>
        where TUserLogin : IdentityUserLogin<TKey>
        where TRoleClaim : IdentityRoleClaim<TKey>
        where TUserToken : IdentityUserToken<TKey>
    {
        public IDbConnection Connection { get; }

        public string UsersTableName { get; }
        public string UserRolesTableName { get; }
        public string UserTokensTableName { get; }
        public string UserLoginsTableName { get; }
        public string UserClaimsTableName { get; }
        public string RolesTableName { get; }
        public string RoleClaimsTableName { get; }

        public DapperContext(IDbConnection connection)
        {
            Connection = connection;

            UsersTableName = GetTableNameFromAttributeOrDefault<TUser>() ?? DefaultTableNames.Users;
            UserRolesTableName = GetTableNameFromAttributeOrDefault<TUserRole>() ?? DefaultTableNames.UserRoles;
            UserTokensTableName = GetTableNameFromAttributeOrDefault<TUserToken>() ?? DefaultTableNames.UserTokens;
            UserLoginsTableName = GetTableNameFromAttributeOrDefault<TUserLogin>() ?? DefaultTableNames.UserLogins;
            RolesTableName = GetTableNameFromAttributeOrDefault<TRole>() ?? DefaultTableNames.Roles;
            RoleClaimsTableName = GetTableNameFromAttributeOrDefault<TRoleClaim>() ?? DefaultTableNames.RoleClaims;
            UserClaimsTableName = GetTableNameFromAttributeOrDefault<TUserClaim>() ?? DefaultTableNames.UserClaims;
        }

        protected DapperContext()
        { }

        private string GetTableNameFromAttributeOrDefault<TEntity>()
        {
            var tableAttr = typeof(TEntity).GetTypeInfo().GetCustomAttribute<TableAttribute>();

            return tableAttr?.Name;
        }
    }
}
