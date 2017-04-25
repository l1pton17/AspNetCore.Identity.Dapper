using System;
using System.Reflection;
using AspNetCore.Identity.Dapper.Entities;
using AspNetCore.Identity.Dapper.PostgreSql.Repositories;
using AspNetCore.Identity.Dapper.Repositories;
using AspNetCore.Identity.Dapper.Stores;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AspNetCore.Identity.Dapper.PostgreSql.DependencyInjection
{
    public static class IdentityDapperPostgreSqlServiceCollectionExtensions
    {
        public static IdentityBuilder AddDapperPostgreSql(this IdentityBuilder builder)
        {
            builder.Services.AddSingleton<IConnectionFactory, NpgsqlConnectionFactory>();

            return builder;
        }
    }
}