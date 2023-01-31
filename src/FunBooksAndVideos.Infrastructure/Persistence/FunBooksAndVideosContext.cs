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
        public DbSet<Video>? Videos { get; set; }
        public DbSet<Book>? Books { get; set; }
        public DbSet<Order>? Orders { get; set; }
    }
}