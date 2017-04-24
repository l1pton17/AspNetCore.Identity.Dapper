using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace AspNetCore.Identity.Dapper
{
    partial class UserStore<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TUserToken, TRoleClaim> : IUserClaimStore<TUser>
    {
        protected abstract TUserClaim CreateUserClaim(TUser user, Claim claim);

        public virtual Task AddClaimsAsync(TUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfInvalidState(cancellationToken);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (claims == null)
            {
                throw new ArgumentNullException(nameof(claims));
            }

            return _userClaimRepository.InsertManyAsync(
                claims.Select(claim => CreateUserClaim(user, claim)));
        }

        public virtual async Task<IList<Claim>> GetClaimsAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfInvalidState(cancellationToken);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var userClaims = await _userClaimRepository.FindAsync(user.Id);

            return userClaims.Select(v => v.ToClaim()).ToList();
        }

        public virtual async Task<IList<TUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfInvalidState(cancellationToken);

            if (claim == null)
            {
                throw new ArgumentNullException(nameof(claim));
            }

            return (await _userClaimRepository.FindUsersForClaimAsync(claim)).ToList();
        }

        public virtual Task RemoveClaimsAsync(TUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfInvalidState(cancellationToken);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (claims == null)
            {
                throw new ArgumentNullException(nameof(claims));
            }

            return _userClaimRepository.DeleteAsync(user.Id, claims);
        }

        public virtual Task ReplaceClaimAsync(TUser user, Claim claim, Claim newClaim, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfInvalidState(cancellationToken);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (claim == null)
            {
                throw new ArgumentNullException(nameof(claim));
            }

            if (newClaim == null)
            {
                throw new ArgumentNullException(nameof(newClaim));
            }

            return _userClaimRepository.UpdateAsync(user.Id, claim, newClaim);
        }
    }
}
