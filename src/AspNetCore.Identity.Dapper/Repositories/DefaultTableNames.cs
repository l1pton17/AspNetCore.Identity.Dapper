using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.Identity.Dapper.Repositories
{
    public static class DefaultTableNames
    {
        public const string RoleClaims = "RoleClaims";
        public const string Roles = "Roles";
        public const string UserClaims = "UserClaims";
        public const string UserLogins = "UserLogins";
        public const string Users = "Users";
        public const string UserRoles = "UserRoles";
        public const string UserTokens = "UserTokens";
    }
}
