using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AspNetCore.Identity.Dapper
{
    partial class UserStore<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TUserToken, TRoleClaim> : IUserPhoneNumberStore<TUser>
    {
        public virtual Task<string> GetPhoneNumberAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfInvalidState(cancellationToken);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.PhoneNumber);
        }

        public virtual Task<bool> GetPhoneNumberConfirmedAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfInvalidState(cancellationToken);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        public virtual Task SetPhoneNumberAsync(TUser user, string phoneNumber, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfInvalidState(cancellationToken);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return _userRepository.SetPhoneNumberAsync(user, phoneNumber);
        }

        public virtual Task SetPhoneNumberConfirmedAsync(TUser user, bool confirmed, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfInvalidState(cancellationToken);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return _userRepository.SetPhoneNumberConfirmedAsync(user, confirmed);
        }
    }
}
