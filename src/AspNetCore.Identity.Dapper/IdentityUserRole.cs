using System;

namespace AspNetCore.Identity.Dapper
{
    public class IdentityUserRole<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey UserId { get; set; }
        public TKey RoleId { get; set; }
    }
}