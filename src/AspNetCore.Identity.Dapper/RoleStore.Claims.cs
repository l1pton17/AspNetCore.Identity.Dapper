using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AspNetCore.Identity.Dapper
{
    partial class RoleStore<TRole, TKey, TUserRole, TRoleClaim>
    {

        public Task GetClaimsAsync(TRole role, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        public Task AddClaimAsync(TRole role, System.Security.Claims.Claim claim,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveClaimAsync(TRole role, System.Security.Claims.Claim claim,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        Task<IList<Claim>> IRoleClaimStore<TRole>.GetClaimsAsync(TRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}