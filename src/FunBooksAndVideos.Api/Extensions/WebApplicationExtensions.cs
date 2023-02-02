using Microsoft.EntityFrameworkCore;
using FunBooksAndVideos.Infrastructure.Persistence;

namespace FunBooksAndVideos.API.Extensions
{
    public static class WebApplicationExtensions
    {
        public static void MigrateDatabase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<FunBooksAndVideosContext>();

            context.Database.Migrate();
        }

        public static void SeedDatabase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<FunBooksAndVideosContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<FunBooksAndVideosContext>>();

            FunBooksAndVideosContextSeed.SeedAsync(context, logger).Wait();
        }
    }
}