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
            services.AddScoped<IBlogPostRepo, BlogPostRepository>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPostService, PostService>();

            services.AddAutoMapper(typeof(UserProfile),typeof(PostProfiles));

            return services;
        }
    }
}
