using System;
using System.Collections.Generic;
using System.Text;
using AspNetCore.Identity.Dapper.Repositories;
using Microsoft.AspNetCore.Identity;

namespace AspNetCore.Identity.Dapper
{
    public partial class RoleStore<TRole, TKey, TUserRole, TRoleClaim>
        where TRole : IdentityRole<TKey, TUserRole, TRoleClaim>
        where TKey : IEquatable<TKey>
        where TUserRole : IdentityUserRole<TKey>
        where TRoleClaim : IdentityRoleClaim<TKey>, new()
    {
        private readonly RoleRepository<TRole, TKey, TUserRole, TRoleClaim> _roleRepository;
        private readonly RoleClaimRepository<TRoleClaim, TKey> _roleClaimRepository;

        private bool _disposed;

        public IdentityErrorDescriber ErrorDescriber { get; set; }

        public RoleStore(IdentityErrorDescriber describer = null)
        {
            _roleRepository = new RoleRepository<TRole, TKey, TUserRole, TRoleClaim>(new DbManager(null));
            _roleClaimRepository = new RoleClaimRepository<TRoleClaim, TKey>(new DbManager(null));

            ErrorDescriber = describer ?? new IdentityErrorDescriber();
        }

        public void Dispose()
        {
            _disposed = true;
        }

        protected void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }
    }
}
