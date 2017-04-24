using System;
using AspNetCore.Identity.Dapper.Repositories;
using Dapper.Contrib.Extensions;

namespace AspNetCore.Identity.Dapper
{
    [Table(DefaultTableNames.UserRoles)]
    public class IdentityUserRole<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey UserId { get; set; }
        public TKey RoleId { get; set; }
    }
}