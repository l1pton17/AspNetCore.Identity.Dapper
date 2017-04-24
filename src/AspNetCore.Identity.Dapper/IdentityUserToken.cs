using System;
using AspNetCore.Identity.Dapper.Repositories;
using Dapper.Contrib.Extensions;

namespace AspNetCore.Identity.Dapper
{
    [Table(DefaultTableNames.UserTokens)]
    public class IdentityUserToken<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Gets or sets the primary key of the user that the token belongs to.
        /// </summary>
        public TKey UserId { get; set; }

        /// <summary>
        /// Gets or sets the LoginProvider this token is from.
        /// </summary>
        public string LoginProvider { get; set; }

        /// <summary>
        /// Gets or sets the name of the token.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the token value.
        /// </summary>
        public string Value { get; set; }
    }
}