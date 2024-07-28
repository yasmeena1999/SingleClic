using Microsoft.EntityFrameworkCore;
using SigleClic.Data.Context;
using System;

namespace SingleClic.Api.Configurations
{
    public static class DBConfig
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BloggingContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection") ??
                    throw new InvalidOperationException("Connection String is not found"));
            });

            return services;
        }
    }
}
