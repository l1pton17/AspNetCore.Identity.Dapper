using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspNetCore.Identity.Dapper.Repositories;
using Microsoft.AspNetCore.Identity;

namespace AspNetCore.Identity.Dapper
{
    public abstract partial class UserStore<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TUserToken, TRoleClaim>
        where TKey : IEquatable<TKey>
        where TUser : IdentityUser<TKey, TUserClaim, TUserRole, TUserLogin, TUserToken>
        where TRole : IdentityRole<TKey, TUserRole, TRoleClaim>
        where TUserClaim : IdentityUserClaim<TKey>, new()
        where TUserRole : IdentityUserRole<TKey>, new()
        where TUserLogin : IdentityUserLogin<TKey>, new()
        where TUserToken : IdentityUserToken<TKey>, new()
        where TRoleClaim : IdentityRoleClaim<TKey>, new()
    {
        private readonly UserRepository<TUser, TKey, TUserClaim, TUserRole, TUserLogin, TUserToken> _userRepository;
        private readonly UserLoginRepository<TUserLogin, TKey> _userLoginRepository;
        private readonly UserTokenRepository<TUserToken, TKey> _userTokenRepository;

        private bool _disposed;

        public IdentityErrorDescriber ErrorDescriber { get; set; }

        protected UserStore(IdentityErrorDescriber describer = null)
        {
            ErrorDescriber = describer ?? new IdentityErrorDescriber();
            _userRepository =
                new UserRepository<TUser, TKey, TUserClaim, TUserRole, TUserLogin, TUserToken>(new DbManager(null));
            _userLoginRepository = new UserLoginRepository<TUserLogin, TKey>(new DbManager(null));
            _userTokenRepository = new UserTokenRepository<TUserToken, TKey>(new DbManager(null));
        }

        public virtual TKey ConvertIdFromString(string id)
        {
            if (id == null)
            {
                return default(TKey);
            }
            return (TKey)TypeDescriptor.GetConverter(typeof(TKey)).ConvertFromInvariantString(id);
        }

        public virtual string ConvertIdToString(TKey id)
        {
            if (Object.Equals(id, default(TKey)))
            {
                return null;
            }
            return id.ToString();
        }

        public void Dispose()
        {
            _disposed = true;
        }

        protected void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        protected void ThrowIfInvalidState(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
        }
    }
}
