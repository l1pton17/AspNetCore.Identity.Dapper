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

        public Task<TUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            ThrowIfInvalidState(cancellationToken);

            var id = ConvertIdFromString(userId);

            return _userRepository.FindByIdAsync(id);
        }

        public Task<TUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            ThrowIfInvalidState(cancellationToken);

            return _userRepository.FindByNameAsync(normalizedUserName);
        }

        public Task<string> GetNormalizedUserNameAsync(TUser user, CancellationToken cancellationToken)
        {
            ThrowIfInvalidState(cancellationToken);
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetUserIdAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfInvalidState(cancellationToken);
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(ConvertIdToString(user.Id));
        }

        public Task<string> GetUserNameAsync(TUser user, CancellationToken cancellationToken)
        {
            ThrowIfInvalidState(cancellationToken);
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.UserName);
        }

        public Task SetNormalizedUserNameAsync(TUser user, string normalizedName, CancellationToken cancellationToken)
        {
            ThrowIfInvalidState(cancellationToken);
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return _userRepository.SetNormalizedUserNameAsync(user, normalizedName);
        }

        public Task SetUserNameAsync(TUser user, string userName, CancellationToken cancellationToken)
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
