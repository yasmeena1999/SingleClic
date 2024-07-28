using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System;
using SingleClic.Core.Models;
using SigleClic.Data.Context;

namespace SingleClic.Api.Configurations
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddCustomIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;
                options.User.RequireUniqueEmail = true;
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 ";
            })
            .AddEntityFrameworkStores<BloggingContext>()
            .AddSignInManager()
            .AddRoles<IdentityRole>();

            return services;
        }
    }
}
