using System;
using System.Reflection;
using AspNetCore.Identity.Dapper.Entities;
using AspNetCore.Identity.Dapper.Stores;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AspNetCore.Identity.Dapper.DependencyInjection
{
    public static class IdentityDapperServiceCollectionExtensions
    {
        public static IdentityBuilder AddDapperStores(this IdentityBuilder builder)
        {
            AddStores(builder.Services, builder.UserType, builder.RoleType);

            return builder;
        }

        private static void AddStores(this IServiceCollection services, Type userType, Type roleType)
        {
            var identityUserType = FindGenericBaseType(userType, typeof(IdentityUser<,,,,>));
            if (identityUserType == null)
            {
                throw new InvalidOperationException();//TODO: add message
            }

            var identityRoleType = FindGenericBaseType(roleType, typeof(IdentityRole<,,>));
            if (identityRoleType == null)
            {
                throw new InvalidOperationException();//TODO: add message
            }

            services.TryAddScoped(
                typeof(IUserStore<>).MakeGenericType(userType),
                typeof(UserStore<,,,,,,,>).MakeGenericType(userType, roleType,
                    identityUserType.GenericTypeArguments[0],
                    identityUserType.GenericTypeArguments[1],
                    identityUserType.GenericTypeArguments[2],
                    identityUserType.GenericTypeArguments[3],
                    identityUserType.GenericTypeArguments[4],
                    identityRoleType.GenericTypeArguments[2]
                ));

            services.TryAddScoped(
                typeof(IRoleStore<>).MakeGenericType(roleType),
                typeof(RoleStore<,,,>).MakeGenericType(roleType,
                    identityRoleType.GenericTypeArguments[0],
                    identityRoleType.GenericTypeArguments[1],
                    identityRoleType.GenericTypeArguments[2]));
        }

        private static TypeInfo FindGenericBaseType(Type currentType, Type genericBaseType)
        {
            var type = currentType.GetTypeInfo();

            while (type.BaseType != null)
            {
                type = type.BaseType.GetTypeInfo();

                var genericType = type.IsGenericType ? type.GetGenericTypeDefinition() : null;
                if (genericType != null && genericType == genericBaseType)
                {
                    return type;
                }
            }

            return null;
        }
    }
}