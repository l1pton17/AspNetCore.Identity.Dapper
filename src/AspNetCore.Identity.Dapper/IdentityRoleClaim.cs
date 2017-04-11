using System;
using System.Security.Claims;

namespace AspNetCore.Identity.Dapper
{
    public class IdentityRoleClaim<TKey>
        where TKey : IEquatable<TKey>
    {
        public int Id { get; set; }

        public TKey RoleId { get; set; }

        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }

        public virtual Claim ToClaim()
            => new Claim(ClaimType, ClaimValue);

        public virtual void InitializeFromClaim(Claim other)
        {
            ClaimType = other?.Type;
            ClaimValue = other?.Value;
        }
    }
}