using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AspNetCore.Identity.Dapper.Entities;
using Dapper;
using Dapper.Contrib.Extensions;

namespace AspNetCore.Identity.Dapper.Repositories
{
    public class UserRoleRepository<TUserRole, TKey> : RepositoryBase<TUserRole, TKey>
        where TUserRole : IdentityUserRole<TKey>
        where TKey : IEquatable<TKey>
    {
        public UserRoleRepository(IConnectionFactory connectionFactory, ITableConfiguration configuration)
            : base(connectionFactory, configuration, configuration.UserRolesTableName)
        {
        }

        public Task InsertAsync(TUserRole userRole)
        {
            return Configuration.Connection.InsertAsync(userRole);
        }

        public Task<IEnumerable<string>> FindRoleNamesAsync(TKey userId)
        {
            return Configuration.Connection.QueryAsync<string>(
                $@"SELECT roles.Name
                   FROM {Configuration.RolesTableName} roles JOIN {TableName} userRoles
                        ON roles.Id = userRoles.RoleId AND userRoles.UserId=@UserId",
                new {UserId = userId});
        }

        public Task<bool> ExistsAsync(TKey userId, TKey roleId)
        {
            return Configuration.Connection.QueryFirstAsync<bool>(
                $@"SELECT COUNT(1)
                   FROM {TableName}
                   WHERE UserId=@UserId AND RoleId=@RoleId",
                new { UserId = userId, RoleId = roleId });
        }

        public Task DeleteAsync(TKey userId, TKey roleId)
        {
            return Configuration.Connection.ExecuteAsync(
                $@"DELETE FROM {TableName}
                   WHERE UserId=@UserId AND RoleId=@RoleId",
                new {UserId = userId, RoleId = roleId});
        }
    }
}
