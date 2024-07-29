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
            services.AddScoped<ICommentRepo, CommentRepository>();
            services.AddScoped<IFollowRepository, FollowRepository>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IFollowService, FollowService>();
            services.AddScoped<ICommentService, CommentService>();

            services.AddAutoMapper(typeof(UserProfile),typeof(PostProfiles),typeof(CommentProfiles));

            return services;
        }
    }
}
