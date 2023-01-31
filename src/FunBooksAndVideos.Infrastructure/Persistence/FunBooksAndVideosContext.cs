using FunBooksAndVideos.Domain;
using Microsoft.EntityFrameworkCore;

namespace FunBooksAndVideos.Infrastructure.Persistence
{
    public class FunBooksAndVideosContext : DbContext
    {
        public FunBooksAndVideosContext(DbContextOptions<FunBooksAndVideosContext> options)
            : base(options)
        {
        }

        public DbSet<Customer>? Customers { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<DigitalProduct>? DigitalProducts { get; set; }
        public DbSet<PhysicalProduct>? PhysicalProducts { get; set; }
        public DbSet<Membership>? Memberships { get; set; }
        public DbSet<Order>? Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfigurationsFromAssembly(typeof(FunBooksAndVideosContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}