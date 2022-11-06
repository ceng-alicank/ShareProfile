using Application.Services.CachingService;
using Application.Services.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.CachingService;
using Persistence.Contexts;
using Persistence.Repositories;

namespace Persistence
{
    public static class PersistenceServiceRegistiration
    {
        public static void AddPersistenceInfrastructurer(this IServiceCollection services, IConfiguration _configuration)
        {
            services.AddDbContext<AppDbContext>
                 (options => options.UseSqlServer(_configuration.GetConnectionString("DbConnectionString")));

            services.AddScoped<IUserRepository  , UserRepository>();
            services.AddScoped<IUserProfileRepository , UserProfileRepository>();
            services.AddScoped<IOperationClaimRepository, OperationClaimRepository>();
            services.AddScoped<IRefreshTokenRepository , RefreshTokenRepository>();
            services.AddScoped<ICacheService, CacheService>();


        }
    }
}
