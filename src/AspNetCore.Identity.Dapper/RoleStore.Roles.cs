using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AspNetCore.Identity.Dapper.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Internal;

namespace AspNetCore.Identity.Dapper
{
    partial class RoleStore<TRole, TKey, TUserRole, TRoleClaim> :
        IRoleStore<TRole>
    {
        public async Task<IdentityResult> CreateAsync(TRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (role == null) throw new ArgumentNullException(nameof(role));

            try
            {
                await _roleRepository.InsertAsync(role);
            }
            catch (Exception)
            {
                return IdentityResult.Failed();
            }

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateAsync(TRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (role == null) throw new ArgumentNullException(nameof(role));

            try
            {
                role.ConcurrencyStamp = Guid.NewGuid().ToString();
                await _roleRepository.UpdateAsync(role);
            }
            catch (Exception)
            {
                return IdentityResult.Failed(ErrorDescriber.DefaultError());
            }

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(TRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (role == null) throw new ArgumentNullException(nameof(role));

            try
            {
                await _roleRepository.DeleteAsync(role);
            }
            catch (Exception)
            {
                return IdentityResult.Failed(ErrorDescriber.DefaultError());
            }

            return IdentityResult.Success;
        }

        public Task<string> GetRoleIdAsync(TRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (role == null) throw new ArgumentNullException(nameof(role));

            return Task.FromResult(ConvertIdToString(role.Id));
        }

        public Task<string> GetRoleNameAsync(TRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (role == null) throw new ArgumentNullException(nameof(role));

            return Task.FromResult(role.Name);
        }

        public Task SetRoleNameAsync(TRole role, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (role == null) throw new ArgumentNullException(nameof(role));

            role.Name = roleName;
            return _roleRepository.UpdateAsync(role);
        }

        public Task<string> GetNormalizedRoleNameAsync(TRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (role == null) throw new ArgumentNullException(nameof(role));

            return Task.FromResult(role.NormalizedName);
        }

        public Task SetNormalizedRoleNameAsync(TRole role, string normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (role == null) throw new ArgumentNullException(nameof(role));

            role.NormalizedName = normalizedName;

            return _roleRepository.UpdateAsync(role);
        }

        public Task<TRole> FindByIdAsync(string id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            var roleId = ConvertIdFromString(id);
            return _roleRepository.FindByIdAsync(roleId);
        }

        public Task<TRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            return _roleRepository.FindByNameAsync(normalizedRoleName);
        }

        public virtual TKey ConvertIdFromString(string id)
        {
            if (id == null)
            {
                return default(TKey);
            }

            return (TKey) TypeDescriptor.GetConverter(typeof(TKey)).ConvertFromInvariantString(id);
        }

        public virtual string ConvertIdToString(TKey id)
        {
            if (id.Equals(default(TKey)))
            {
                return null;
            }

            return id.ToString();
        }
    }
}