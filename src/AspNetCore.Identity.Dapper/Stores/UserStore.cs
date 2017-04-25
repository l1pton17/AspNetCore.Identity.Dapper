using System;
using System.ComponentModel;
using System.Security.Claims;
using System.Threading;
using AspNetCore.Identity.Dapper.Entities;
using AspNetCore.Identity.Dapper.Repositories;
using Microsoft.AspNetCore.Identity;

namespace AspNetCore.Identity.Dapper.Stores
{
    public class UserStore : UserStore<IdentityUser<string>>
    {
        public UserStore(ITableConfiguration context, IdentityErrorDescriber describer = null) : base(context, describer) { }
    }

    public class UserStore<TUser> : UserStore<TUser, IdentityRole, string>
        where TUser : IdentityUser<string>, new()
    {
        public UserStore(ITableConfiguration context, IdentityErrorDescriber describer = null) : base(context, describer) { }
    }

    public class UserStore<TUser, TRole> : UserStore<TUser, TRole, string>
        where TUser : IdentityUser<string>
        where TRole : IdentityRole<string>
    {
        public UserStore(ITableConfiguration context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }
    }

    public class UserStore<TUser, TRole, TKey> :
        UserStore<TUser, TRole, TKey, IdentityUserClaim<TKey>, IdentityUserRole<TKey>, IdentityUserLogin<TKey>, IdentityUserToken<TKey>, IdentityRoleClaim<TKey>>
        where TUser : IdentityUser<TKey>
        where TRole : IdentityRole<TKey>
        where TKey : IEquatable<TKey>
    {
        public UserStore(ITableConfiguration context, IdentityErrorDescriber describer = null)
            : base(context, describer)
        { }

        protected override IdentityUserRole<TKey> CreateUserRole(TUser user, TRole role)
        {
            return new IdentityUserRole<TKey>
            {
                UserId = user.Id,
                RoleId = role.Id
            };
        }

        protected override IdentityUserClaim<TKey> CreateUserClaim(TUser user, Claim claim)
        {
            var userClaim = new IdentityUserClaim<TKey> { UserId = user.Id };
            userClaim.InitializeFromClaim(claim);

            return userClaim;
        }

        protected override IdentityUserToken<TKey> CreateUserToken(TUser user, string loginProvider, string name, string value)
        {
            return new IdentityUserToken<TKey>
            {
                UserId = user.Id,
                LoginProvider = loginProvider,
                Name = name,
                Value = value
            };
        }

        protected override IdentityUserLogin<TKey> CreateUserLogin(TUser user, UserLoginInfo login)
        {
            return new IdentityUserLogin<TKey>
            {
                UserId = user.Id,
                LoginProvider = login.LoginProvider,
                ProviderKey = login.ProviderKey,
                ProviderDisplayName = login.ProviderDisplayName
            };
        }
    }

    public abstract partial class UserStore<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TUserToken, TRoleClaim>
        where TKey : IEquatable<TKey>
        where TUser : IdentityUser<TKey, TUserClaim, TUserRole, TUserLogin>
        where TRole : IdentityRole<TKey, TUserRole, TRoleClaim>
        where TUserClaim : IdentityUserClaim<TKey>, new()
        where TUserRole : IdentityUserRole<TKey>, new()
        where TUserLogin : IdentityUserLogin<TKey>, new()
        where TUserToken : IdentityUserToken<TKey>, new()
        where TRoleClaim : IdentityRoleClaim<TKey>, new()
    {
        private readonly UserRepository<TUser, TKey, TUserClaim, TUserRole, TUserLogin> _userRepository;
        private readonly UserLoginRepository<TUserLogin, TKey> _userLoginRepository;
        private readonly UserTokenRepository<TUserToken, TKey> _userTokenRepository;
        private readonly UserClaimRepository<TUser, TUserClaim, TKey> _userClaimRepository;
        private readonly UserRoleRepository<TUserRole, TKey> _userRoleRepository;
        private readonly RoleRepository<TRole, TKey, TUserRole, TRoleClaim> _roleRepository;

        private bool _disposed;

        public IdentityErrorDescriber ErrorDescriber { get; set; }

        protected UserStore(ITableConfiguration context, IdentityErrorDescriber describer = null)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            ErrorDescriber = describer ?? new IdentityErrorDescriber();

            _userRepository = new UserRepository<TUser, TKey, TUserClaim, TUserRole, TUserLogin>(context);
            _userLoginRepository = new UserLoginRepository<TUserLogin, TKey>(context);
            _userTokenRepository = new UserTokenRepository<TUserToken, TKey>(context);
            _userClaimRepository = new UserClaimRepository<TUser, TUserClaim, TKey>(context);
            _userRoleRepository = new UserRoleRepository<TUserRole, TKey>(context);
            _roleRepository = new RoleRepository<TRole, TKey, TUserRole, TRoleClaim>(context);
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
