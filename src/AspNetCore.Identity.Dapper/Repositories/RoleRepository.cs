using System;
using System.Threading.Tasks;
using Dapper;

namespace AspNetCore.Identity.Dapper.Repositories
{
    public class RoleRepository<TRole, TKey, TUserRole, TRoleClaim>
        where TKey: IEquatable<TKey>
        where TRole : IdentityRole<TKey, TUserRole, TRoleClaim>
        where TUserRole : IdentityUserRole<TKey>
        where TRoleClaim : IdentityRoleClaim<TKey>
    {
        private readonly DbManager _database;

        public RoleRepository(DbManager database)
        {
            _database = database;
        }

        public void Add(TRole role)
        {
            _database.Connection.ExecuteAsync(
                "INSERT INTO Role (Id, Name, NormalizedName, ConcurrencyStamp) values (@Id, @Name, @NormalizedName, @ConcurrencyStamp",
                new
                {
                    Id = role.Id,
                    Name = role.Name,
                    NormalizedName = role.NormalizedName,
                    ConcurrencyStamp = role.ConcurrencyStamp
                });
        }

        public async Task<TRole> GetRoleByNameAsync(string roleName)
        {
            var roleId = await GetRoleIdAsync(roleName);

            if (!roleId.Equals(default(TKey)))
            {
                return new IdentityRole<TKey>(roleName);
            }

            return null;
        }

        public Task<TKey> GetRoleIdAsync(string roleName)
        {
            return _database.Connection.ExecuteScalarAsync<TKey>(
                "select Id from Role where Name=@Name",
                new {Name = roleName});
        }
    }
}