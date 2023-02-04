using FunBooksAndVideos.Domain;

namespace FunBooksAndVideos.Application.Tests.Mothers
{
    internal static class ProductsMother
    {
        public static PhysicalProduct NonExistingPhysicalProduct => new
        (
            id: Guid.NewGuid(),
            name: nameof(NonExistingPhysicalProduct),
            description: "Something not worth your money",
            price: 0,
            productType: ProductType.Book,
            quantity: 1,
            size: "10/20/5"
        );
    }
}