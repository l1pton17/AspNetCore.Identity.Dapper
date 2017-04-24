using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AspNetCore.Identity.Dapper
{
    partial class UserStore<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TUserToken, TRoleClaim> : IUserStore<TUser>
    {
        public virtual async Task<IdentityResult> CreateAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfInvalidState(cancellationToken);
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            await _userRepository.InsertAsync(user);

            return IdentityResult.Success;
        }

        public virtual async Task<IdentityResult> DeleteAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfInvalidState(cancellationToken);
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            try
            {
                await _userRepository.DeleteAsync(user);
            }
            catch (Exception)
            {
                return IdentityResult.Failed(ErrorDescriber.DefaultError());
            }

            return IdentityResult.Success;
        }

        public virtual Task<TUser> FindByIdAsync(string userId, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfInvalidState(cancellationToken);

            var id = ConvertIdFromString(userId);

            return _userRepository.FindByIdAsync(id);
        }

        public virtual Task<TUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfInvalidState(cancellationToken);

            return _userRepository.FindByNameAsync(normalizedUserName);
        }

        public virtual Task<string> GetNormalizedUserNameAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfInvalidState(cancellationToken);
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.NormalizedUserName);
        }

        public virtual Task<string> GetUserIdAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfInvalidState(cancellationToken);
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(ConvertIdToString(user.Id));
        }

        public virtual Task<string> GetUserNameAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfInvalidState(cancellationToken);
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.UserName);
        }

        public virtual Task SetNormalizedUserNameAsync(TUser user, string normalizedName, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfInvalidState(cancellationToken);
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return _userRepository.SetNormalizedUserNameAsync(user, normalizedName);
        }

        public virtual Task SetUserNameAsync(TUser user, string userName, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfInvalidState(cancellationToken);
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return _userRepository.SetUserNameAsync(user, userName);
        }

        public virtual async Task<IdentityResult> UpdateAsync(TUser user, CancellationToken cancellationToken)
        {
            ThrowIfInvalidState(cancellationToken);
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            user.ConcurrencyStamp = Guid.NewGuid().ToString();
            try
            {
                await _userRepository.UpdateAsync(user);
            }
            catch (Exception)
            {
                return IdentityResult.Failed(ErrorDescriber.DefaultError());
            }

            return IdentityResult.Success;
        }
    }
}
