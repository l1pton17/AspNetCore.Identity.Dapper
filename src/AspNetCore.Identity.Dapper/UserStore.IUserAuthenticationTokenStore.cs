using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AspNetCore.Identity.Dapper
{
    partial class UserStore<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TUserToken, TRoleClaim> : IUserAuthenticationTokenStore<TUser>
    {
        protected abstract TUserToken CreateUserToken(TUser user, string loginProvider, string name, string value);

        private Task<TUserToken> FindTokenOrDefaultAsync(TUser user, string loginProvider, string name)
        {
            return _userTokenRepository.FindOrDefaultAsync(user.Id, loginProvider, name);
        }

        public virtual async Task<string> GetTokenAsync(TUser user, string loginProvider, string name, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfInvalidState(cancellationToken);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var entry = await FindTokenOrDefaultAsync(user, loginProvider, name);

            return entry?.Value;
        }

        public virtual Task RemoveTokenAsync(TUser user, string loginProvider, string name, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfInvalidState(cancellationToken);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return _userTokenRepository.DeleteAsync(user.Id, loginProvider, name);
        }

        public virtual async Task SetTokenAsync(TUser user, string loginProvider, string name, string value, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfInvalidState(cancellationToken);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var token = await FindTokenOrDefaultAsync(user, loginProvider, name);
            if (token == null)
            {
                await _userTokenRepository.InsertAsync(CreateUserToken(user, loginProvider, name, value));
            }
            else
            {
                await _userTokenRepository.SetTokenValueAsync(token, value);
            }
        }
    }
}
