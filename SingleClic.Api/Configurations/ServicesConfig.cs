using SigleClic.Data.IRepos;
using SigleClic.Data.Repos;
using SingleClic.Core.MappingProfiles;
using SingleClic.Services.IServices;
using SingleClic.Services.Services;

namespace SingleClic.Api.Configurations
{
    public static class ServicesConfig
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            
            services.AddScoped<IAuthService, AuthService>();
            services.AddAutoMapper(typeof(UserProfile));

            return services;
        }
    }
}
