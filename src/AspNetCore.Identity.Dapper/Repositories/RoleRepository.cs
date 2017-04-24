using System;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;

namespace AspNetCore.Identity.Dapper.Repositories
{
    public class RoleRepository<TRole, TKey, TUserRole, TRoleClaim>
        where TKey: IEquatable<TKey>
        where TRole : IdentityRole<TKey, TUserRole, TRoleClaim>
        where TUserRole : IdentityUserRole<TKey>
        where TRoleClaim : IdentityRoleClaim<TKey>
    {
        private readonly DbManager _database;
        //TODO: remove in a refactoring stage
        public string TableName { get; } = "Roles";

        public RoleRepository(DbManager database)
        {
            _database = database;
        }

        public Task InsertAsync(TRole role)
        {
            return _database.Connection.InsertAsync(role);
        }

        public Task DeleteAsync(TRole role)
        {
            return _database.Connection.DeleteAsync(role);
        }

        public Task<TRole> FindByNameAsync(string roleName)
        {
            return _database.Connection.ExecuteScalarAsync<TRole>(
                $"SELECT * FROM {TableName} WHERE Name=@name",
                new {Name = roleName});
        }

        public Task<TRole> FindByIdAsync(TKey roleId)
        {
            return _database.Connection.ExecuteScalarAsync<TRole>(
                $"SELECT * FROM {TableName} WHERE Id=@Id",
                new {Id = roleId});
        }

        public Task UpdateAsync(TRole role)
        {
            return _database.Connection.UpdateAsync(role);
        }
    }
}