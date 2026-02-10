using RealPlaza.Domain.Interfaces;
using RealPlaza.Infrastructure.Data;
using RealPlaza.Infrastructure.Repository;

namespace RealPlaza.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
