using FunBooksAndVideos.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Infrastructure;

namespace sFunBooksAndVideos.Infrastructure
{
    public static class DependencyInjection
    {
         public static IServiceCollection AddInfrastructure(this IServiceCollection services, InfrastructureConfig configuration)
        {
            services.AddDbContext<FunBooksAndVideosContext>(options =>
                options.UseSqlServer(configuration.PersistenceConfig?.ConnectionString));

            return services;
        }
    }
}