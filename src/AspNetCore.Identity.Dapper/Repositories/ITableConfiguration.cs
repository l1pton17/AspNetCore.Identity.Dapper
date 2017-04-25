using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AspNetCore.Identity.Dapper.Repositories
{
    public interface ITableConfiguration
    {
        IDbConnection Connection { get; }

        string UsersTableName { get; }
        string UserRolesTableName { get; }
        string UserTokensTableName { get; }
        string UserLoginsTableName { get; }
        string UserClaimsTableName { get; }
        string RolesTableName { get; }
        string RoleClaimsTableName { get; }
    }
}
