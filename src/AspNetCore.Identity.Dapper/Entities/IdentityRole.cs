using System;
using AspNetCore.Identity.Dapper.Repositories;
using Dapper.Contrib.Extensions;

namespace AspNetCore.Identity.Dapper.Entities
{
    public class IdentityRole : IdentityRole<string>
    {
        public IdentityRole()
        {
            Id = Guid.NewGuid().ToString();
        }

        public IdentityRole(string roleName) : this()
        {
            Name = roleName;
        }
    }
    
    public class IdentityRole<TKey> : IdentityRole<TKey, IdentityUserRole<TKey>, IdentityRoleClaim<TKey>>
        where TKey : IEquatable<TKey>
    {
        public IdentityRole() { }

        public IdentityRole(string roleName) : base(roleName) { }
    }

    [Table(DefaultTableNames.Roles)]
    public class IdentityRole<TKey, TUserRole, TRoleClaim>
        where TKey : IEquatable<TKey>
        where TUserRole : IdentityUserRole<TKey>
        where TRoleClaim : IdentityRoleClaim<TKey>
    {
        public TKey Id { get; set; }

        public string Name { get; set; }

        public string NormalizedName { get; set; }

        public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

        public IdentityRole() { }

        public IdentityRole(string roleName) : this()
        {
            Name = roleName;
        }

        public override string ToString() => Name;
    }
}