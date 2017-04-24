using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace AspNetCore.Identity.Dapper
{
    partial class UserStore<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TUserToken, TRoleClaim> : IUserRoleStore<TUser>
    {
        protected abstract TUserRole CreateUserRole(TUser user, TRole role);

        public virtual async Task AddToRoleAsync(TUser user, string normalizedRoleName, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfInvalidState(cancellationToken);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrWhiteSpace(normalizedRoleName))
            {
                throw new ArgumentException("Value cannot be null or empty", nameof(normalizedRoleName));
            }

            var roleEntity = await _roleRepository.FindByNormalizedNameAsync(normalizedRoleName);
            if (roleEntity == null)
            {
                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, "Role {0} not found", normalizedRoleName));
            }

            var userRole = CreateUserRole(user, roleEntity);

            await _userRoleRepository.InsertAsync(userRole);
        }

        public virtual async Task<IList<string>> GetRolesAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfInvalidState(cancellationToken);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var roleNames = await _userRoleRepository.FindRoleNamesAsync(user.Id);

            return roleNames.ToList();
        }

        public virtual async Task<IList<TUser>> GetUsersInRoleAsync(string normalizedRoleName, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfInvalidState(cancellationToken);

            if (String.IsNullOrEmpty(normalizedRoleName))
            {
                throw new ArgumentNullException(nameof(normalizedRoleName));
            }

            var roleEntity = await _roleRepository.FindByNormalizedNameAsync(normalizedRoleName);

            if (roleEntity != null)
            {
                var users = await _userRepository.FindByRoleIdAsync(roleEntity.Id);

                return users.ToList();
            }

            return new List<TUser>();
        }

        public virtual async Task<bool> IsInRoleAsync(TUser user, string normalizedRoleName, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfInvalidState(cancellationToken);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrWhiteSpace(normalizedRoleName))
            {
                throw new ArgumentException("Value cannot be null or empty", nameof(normalizedRoleName));
            }

            var roleEntity = await _roleRepository.FindByNormalizedNameAsync(normalizedRoleName);
            if (roleEntity != null)
            {
                return await _userRoleRepository.ExistsAsync(user.Id, roleEntity.Id);
            }

            return false;
        }

        public virtual async Task RemoveFromRoleAsync(TUser user, string normalizedRoleName, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfInvalidState(cancellationToken);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrWhiteSpace(normalizedRoleName))
            {
                throw new ArgumentException("Value cannot be null or empty", nameof(normalizedRoleName));
            }

            var roleEntity = await _roleRepository.FindByNormalizedNameAsync(normalizedRoleName);
            if (roleEntity != null)
            {
                await _userRoleRepository.DeleteAsync(user.Id, roleEntity.Id);
            }
        }
    }
}
