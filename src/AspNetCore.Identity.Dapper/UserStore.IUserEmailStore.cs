using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AspNetCore.Identity.Dapper
{
    partial class UserStore<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TUserToken, TRoleClaim> : IUserEmailStore<TUser>
    {
        public virtual Task<TUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfInvalidState(cancellationToken);

            return _userRepository.FindByEmailAsync(normalizedEmail);
        }

        public virtual Task<string> GetEmailAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfInvalidState(cancellationToken);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.Email);
        }

        public virtual Task<bool> GetEmailConfirmedAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfInvalidState(cancellationToken);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.EmailConfirmed);
        }

        public virtual Task<string> GetNormalizedEmailAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfInvalidState(cancellationToken);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.NormalizedEmail);
        }

        public virtual Task SetEmailAsync(TUser user, string email, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfInvalidState(cancellationToken);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return _userRepository.SetEmailAsync(user.Id, email);
        }

        public virtual Task SetEmailConfirmedAsync(TUser user, bool confirmed, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfInvalidState(cancellationToken);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return _userRepository.SetEmailConfirmedAsync(user.Id, confirmed);
        }

        public virtual Task SetNormalizedEmailAsync(TUser user, string normalizedEmail, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfInvalidState(cancellationToken);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return _userRepository.SetNormalizedEmailAsync(user.Id, normalizedEmail);
        }
    }
}
