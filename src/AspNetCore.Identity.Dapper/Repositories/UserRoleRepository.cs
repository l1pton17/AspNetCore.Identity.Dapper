using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;

namespace AspNetCore.Identity.Dapper.Repositories
{
    public class UserRoleRepository<TUserRole, TKey> : RepositoryBase<TUserRole, TKey>
        where TUserRole : IdentityUserRole<TKey>
        where TKey : IEquatable<TKey>
    {
        public UserRoleRepository(IDapperContext context)
            : base(context, context.UserRolesTableName)
        {
        }

        public Task InsertAsync(TUserRole userRole)
        {
            return Context.Connection.InsertAsync(userRole);
        }

        public Task<IEnumerable<string>> FindRoleNamesAsync(TKey userId)
        {
            return Context.Connection.QueryAsync<string>(
                $@"SELECT roles.Name
                   FROM {Context.RolesTableName} roles JOIN {TableName} userRoles
                        ON roles.Id = userRoles.RoleId AND userRoles.UserId=@UserId",
                new {UserId = userId});
        }

        public Task<bool> ExistsAsync(TKey userId, TKey roleId)
        {
            return Context.Connection.QueryFirstAsync<bool>(
                $@"SELECT COUNT(1)
                   FROM {TableName}
                   WHERE UserId=@UserId AND RoleId=@RoleId",
                new { UserId = userId, RoleId = roleId });
        }

        public Task DeleteAsync(TKey userId, TKey roleId)
        {
            return Context.Connection.ExecuteAsync(
                $@"DELETE FROM {TableName}
                   WHERE UserId=@UserId AND RoleId=@RoleId",
                new {UserId = userId, RoleId = roleId});
        }
    }
}
