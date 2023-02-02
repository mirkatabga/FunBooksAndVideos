using FunBooksAndVideos.Domain;
using Microsoft.Extensions.Logging;

namespace FunBooksAndVideos.Infrastructure.Persistence
{
    public static class FunBooksAndVideosContextSeed
    {
        public static async Task SeedAsync(FunBooksAndVideosContext context, ILogger<FunBooksAndVideosContext> logger)
        {
            SeedCustomers(context);
            SeedProducts(context);
            SeedMemberships(context);

            await context.SaveChangesAsync();

            logger.LogInformation("Seed database associated with context {DbContextName}", typeof(FunBooksAndVideosContext).Name);
        }

        private static void SeedMemberships(FunBooksAndVideosContext context)
        {
            if (!context.Memberships!.Any())
            {
                context.Memberships!.Add(new Membership(
                    id: Guid.NewGuid(),
                    name: "Book Club",
                    description: "New book each month! A yearly subscription to our book club.",
                    price: 200m
                ));

                context.Memberships!.Add(new Membership(
                    id: Guid.NewGuid(),
                    name: "Video Club",
                    description: "New video each week! A yearly subscription to our video club.",
                    price: 100m
                ));

                context.Memberships!.Add(new Membership(
                    id: Guid.NewGuid(),
                    name: "Premium",
                    description: "Yearly subscription. You receive all benefits from both Book and Video clubs",
                    price: 270m
                ));
            }
        }

        private static void SeedProducts(FunBooksAndVideosContext context)
        {
            if (!context.Products!.Any())
            {
                context.Products!.Add(new PhysicalProduct(
                        id: Guid.NewGuid(),
                        name: "Ego is the Enemy",
                        description: "Written by Ryan Holiday",
                        price: 12.90m,
                        productType: ProductType.Book,
                        quantity: 10,
                        size: "10/20/5"
                    ));

                context.Products!.Add(new DigitalProduct(
                        id: Guid.NewGuid(),
                        name: "Clean Code: A Handbook of Agile Software Craftsmanship",
                        description: "Written by Robert C. Martin",
                        price: 32.00m,
                        productType: ProductType.Book,
                        sizeInMB: 20
                    ));

                context.Products!.Add(new DigitalProduct(
                    id: Guid.NewGuid(),
                    name: "TDD London vs Chicago",
                    description: "Introduction to the two styles of TDD",
                    price: 5.00m,
                    productType: ProductType.Video,
                    sizeInMB: 200
                ));
            }
        }

        private static void SeedCustomers(FunBooksAndVideosContext context)
        {
            if (!context.Customers!.Any())
            {
                context.Customers!.Add(new Customer(
                    id: Guid.NewGuid(),
                    firstName: "John",
                    lastName: "Doe"
                ));

                context.Customers!.Add(new Customer(
                    id: Guid.NewGuid(),
                    firstName: "Jane",
                    lastName: "Doe"
                ));
            }
        }
    }
}