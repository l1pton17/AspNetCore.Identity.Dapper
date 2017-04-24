using System;
using AspNetCore.Identity.Dapper.Repositories;
using Dapper.Contrib.Extensions;

namespace AspNetCore.Identity.Dapper
{
    [Table(DefaultTableNames.UserLogins)]
    public class IdentityUserLogin<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Gets or sets the login provider for the login (e.g. facebook, google)
        /// </summary>
        public string LoginProvider { get; set; }

        /// <summary>
        /// Gets or sets the unique provider identifier for this login.
        /// </summary>
        public string ProviderKey { get; set; }

        /// <summary>
        /// Gets or sets the friendly name used in a UI for this login.
        /// </summary>
        public string ProviderDisplayName { get; set; }

        /// <summary>
        /// Gets or sets the of the primary key of the user associated with this login.
        /// </summary>
        public TKey UserId { get; set; }
    }
}