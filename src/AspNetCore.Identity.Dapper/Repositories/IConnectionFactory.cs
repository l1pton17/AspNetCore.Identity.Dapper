using System.Data;

namespace AspNetCore.Identity.Dapper.Repositories
{
    public interface IConnectionFactory
    {
        IDbConnection Create();
    }
}