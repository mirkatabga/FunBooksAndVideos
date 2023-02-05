using FunBooksAndVideos.Domain;

namespace FunBooksAndVideos.Application.Tests.Mothers
{
    internal static class ProductsMother
    {
        public static PhysicalProduct GetNonExistingPhysicalProduct() => new
        (
            id: Guid.NewGuid(),
            name: "NonExistingPhysicalProduct",
            description: "Something not worth your money",
            price: 0,
            productType: ProductType.Book,
            quantity: 1,
            size: "10/20/5"
        );

        public static PhysicalProduct GetLOTRHardCover() => new
        (
            id: Guid.NewGuid(),
            name: "The Lord of the Rings",
            description: "By J. R. R. Tolkien",
            price: 30,
            ProductType.Book,
            quantity: 40,
            size: "12/10/10"
        );
    }
}