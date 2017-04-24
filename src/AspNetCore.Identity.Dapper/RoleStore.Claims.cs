using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AspNetCore.Identity.Dapper
{
    partial class RoleStore<TRole, TKey, TUserRole, TRoleClaim> : IRoleClaimStore<TRole>
    {
        public virtual async Task<IList<Claim>> GetClaimsAsync(TRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            return (await _roleClaimRepository.FindByRoleIdAsync(role.Id))
                .Select(v => v.ToClaim())
                .ToList();
        }

        public Task AddClaimAsync(TRole role, Claim claim, CancellationToken cancellationToken = new CancellationToken())
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            if (claim == null)
            {
                throw new ArgumentNullException(nameof(claim));
            }

            return _roleClaimRepository.InsertAsync(CreateRoleClaim(role, claim));
        }

        public Task RemoveClaimAsync(TRole role, Claim claim, CancellationToken cancellationToken = new CancellationToken())
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            if (claim == null)
            {
                throw new ArgumentNullException(nameof(claim));
            }

            return _roleClaimRepository.RemoveClaimAsync(role.Id, claim);
        }

        protected virtual TRoleClaim CreateRoleClaim(TRole role, Claim claim)
        {
            return new TRoleClaim
            {
                RoleId = role.Id,
                ClaimType = claim.Type,
                ClaimValue = claim.Value
            };
        }
    }
}