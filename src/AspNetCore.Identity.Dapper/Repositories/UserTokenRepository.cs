using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AspNetCore.Identity.Dapper.Entities;
using Dapper;
using Dapper.Contrib.Extensions;

namespace AspNetCore.Identity.Dapper.Repositories
{
    public class UserTokenRepository<TUserToken, TKey> : RepositoryBase<TUserToken, TKey>
        where TUserToken : IdentityUserToken<TKey>
        where TKey : IEquatable<TKey>
    {
        public UserTokenRepository(IConnectionFactory connectionFactory, ITableConfiguration configuration)
            : base(connectionFactory, configuration, configuration.UserTokensTableName)
        {
        }

        public Task SetTokenValueAsync(TUserToken userToken, string value)
        {
            userToken.Value = value;

            return Configuration.Connection.ExecuteAsync(
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
            return Configuration.Connection.InsertAsync(userToken);
        }

        public Task<TUserToken> FindOrDefaultAsync(TKey userId, string loginProvider, string name)
        {
            return Configuration.Connection.QueryFirstOrDefaultAsync<TUserToken>(
                $@"SELECT *
                   FROM {TableName}
                   WHERE UserId=@UserId AND LoginProvider=@LoginProvider AND Name=@Name",
                new {UserId = userId, LoginProvider = loginProvider, Name = name});
        }

        public Task DeleteAsync(TKey userId, string loginProvider, string name)
        {
            return Configuration.Connection.ExecuteAsync(
                $@"DELETE FROM {TableName}
                   WHERE UserId=@UserId AND LoginProvider=@LoginProvider AND Name=@Name",
                new {UserId = userId, LoginProvider = loginProvider, Name = name});
        }
    }
}
