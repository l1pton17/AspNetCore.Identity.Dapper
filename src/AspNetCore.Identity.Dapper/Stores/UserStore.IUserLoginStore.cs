using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AspNetCore.Identity.Dapper.Stores
{
    partial class UserStore<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TUserToken, TRoleClaim> : IUserLoginStore<TUser>
    {
        protected abstract TUserLogin CreateUserLogin(TUser user, UserLoginInfo login);

        public virtual Task AddLoginAsync(TUser user, UserLoginInfo login, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfInvalidState(cancellationToken);
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (login == null)
            {
                throw new ArgumentNullException(nameof(login));
            }

            var userLogin = CreateUserLogin(user, login);
            return _userLoginRepository.InsertAsync(userLogin);
        }

        public virtual async Task<TUser> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfInvalidState(cancellationToken);

            var userLogin = await _userLoginRepository.FindByProviderOrDefaultAsync(loginProvider, providerKey);
            if (userLogin != null)
            {
                return await _userRepository.FindByIdAsync(userLogin.UserId);
            }

            return null;
        }

        public virtual async Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfInvalidState(cancellationToken);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return (await _userLoginRepository.FindByUserId(user.Id))
                .Select(v => new UserLoginInfo(v.LoginProvider, v.ProviderKey, v.ProviderDisplayName))
                .ToList();
        }

        public virtual Task RemoveLoginAsync(TUser user, string loginProvider, string providerKey, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfInvalidState(cancellationToken);
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return _userLoginRepository.DeleteAsync(user.Id, loginProvider, providerKey);
        }
    }
}
