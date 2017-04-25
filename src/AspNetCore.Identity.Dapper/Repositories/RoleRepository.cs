using System;
using System.Threading.Tasks;
using AspNetCore.Identity.Dapper.Entities;
using Dapper;
using Dapper.Contrib.Extensions;

namespace AspNetCore.Identity.Dapper.Repositories
{
    public class RoleRepository<TRole, TKey, TUserRole, TRoleClaim> : RepositoryBase<TRole, TKey>
        where TKey: IEquatable<TKey>
        where TRole : IdentityRole<TKey, TUserRole, TRoleClaim>
        where TUserRole : IdentityUserRole<TKey>
        where TRoleClaim : IdentityRoleClaim<TKey>
    {
        public RoleRepository(IConnectionFactory connectionFactory, ITableConfiguration configuration)
            : base(connectionFactory, configuration, configuration.RolesTableName)
        {
        }

        public Task InsertAsync(TRole role)
        {
            return Configuration.Connection.InsertAsync(role);
        }

        public Task DeleteAsync(TRole role)
        {
            return Configuration.Connection.DeleteAsync(role);
        }

        public Task<TRole> FindByNameAsync(string roleName)
        {
            return FindByPropertyAsync(nameof(IdentityRole.Name), roleName);
        }

        public Task<TRole> FindByNormalizedNameAsync(string normalizedRoleName)
        {
            return FindByPropertyAsync(nameof(IdentityRole.NormalizedName), normalizedRoleName);
        }

        public Task<TRole> FindByIdAsync(TKey id)
        {
            return FindByPropertyAsync(nameof(IdentityRole.Id), id);
        }

        public Task UpdateAsync(TRole role)
        {
            return Configuration.Connection.UpdateAsync(role);
        }
    }
}