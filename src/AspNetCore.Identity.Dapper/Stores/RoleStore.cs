﻿using System;
using AspNetCore.Identity.Dapper.Entities;
using AspNetCore.Identity.Dapper.Repositories;
using Microsoft.AspNetCore.Identity;

namespace AspNetCore.Identity.Dapper.Stores
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

        public RoleStore(
            IConnectionFactory connectionFactory,
            ITableConfiguration context,
            IdentityErrorDescriber describer = null)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (connectionFactory == null)
            {
                throw new ArgumentNullException(nameof(connectionFactory));
            }

            _roleRepository = new RoleRepository<TRole, TKey, TUserRole, TRoleClaim>(connectionFactory, context);
            _roleClaimRepository = new RoleClaimRepository<TRoleClaim, TKey>(connectionFactory, context);

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
