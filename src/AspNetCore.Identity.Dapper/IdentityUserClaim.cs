using System;
using System.Security.Claims;

namespace AspNetCore.Identity.Dapper
{
    public class IdentityUserClaim<TKey>
        where TKey:IEquatable<TKey>
    {
        /// <summary>
        /// Gets or sets the identifier for this user claim.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the primary key of the user associated with this claim.
        /// </summary>
        public TKey UserId { get; set; }

        /// <summary>
        /// Gets or sets the claim type for this claim.
        /// </summary>
        public string ClaimType { get; set; }

        /// <summary>
        /// Gets or sets the claim value for this claim.
        /// </summary>
        public string ClaimValue { get; set; }

        /// <summary>
        /// Converts the entity into a Claim instance.
        /// </summary>
        /// <returns></returns>
        public virtual Claim ToClaim()
        {
            return new Claim(ClaimType, ClaimValue);
        }

        /// <summary>
        /// Reads the type and value from the Claim.
        /// </summary>
        /// <param name="claim"></param>
        public virtual void InitializeFromClaim(Claim claim)
        {
            ClaimType = claim.Type;
            ClaimValue = claim.Value;
        }
    }
}