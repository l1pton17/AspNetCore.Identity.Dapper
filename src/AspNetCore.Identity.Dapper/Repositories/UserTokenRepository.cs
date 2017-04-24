using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;

namespace AspNetCore.Identity.Dapper.Repositories
{
    public class UserTokenRepository<TUserToken, TKey> : RepositoryBase<TUserToken, TKey>
        where TUserToken : IdentityUserToken<TKey>
        where TKey : IEquatable<TKey>
    {
        public UserTokenRepository(IDapperContext context)
            : base(context, context.UserTokensTableName)
        {
        }

        public Task SetTokenValueAsync(TUserToken userToken, string value)
        {
            userToken.Value = value;

            return Context.Connection.ExecuteAsync(
                $@"UPDATE {TableName}
                   SET Value=@Value
                   WHERE UserId=@UserId AND LoginProvider=@LoginProvider AND Name=@Name",
                new
                {
                    UserId = userToken.UserId,
                    Value = value,
                    LoginProvider = userToken.LoginProvider,
                    Name = userToken.Name
                });
        }

        public Task InsertAsync(TUserToken userToken)
        {
            return Context.Connection.InsertAsync(userToken);
        }

        public Task<TUserToken> FindOrDefaultAsync(TKey userId, string loginProvider, string name)
        {
            return Context.Connection.QueryFirstOrDefaultAsync<TUserToken>(
                $@"SELECT *
                   FROM {TableName}
                   WHERE UserId=@UserId AND LoginProvider=@LoginProvider AND Name=@Name",
                new {UserId = userId, LoginProvider = loginProvider, Name = name});
        }

        public Task DeleteAsync(TKey userId, string loginProvider, string name)
        {
            return Context.Connection.ExecuteAsync(
                $@"DELETE FROM {TableName}
                   WHERE UserId=@UserId AND LoginProvider=@LoginProvider AND Name=@Name",
                new {UserId = userId, LoginProvider = loginProvider, Name = name});
        }
    }
}
