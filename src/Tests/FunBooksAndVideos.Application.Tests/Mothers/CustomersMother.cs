using FunBooksAndVideos.Domain;

namespace FunBooksAndVideos.Application.Tests.Mothers
{
    internal static class CustomersMother
    {
        public static Customer John => new
        (
            id: Guid.NewGuid(),
            firstName: "John",
            lastName: "Doe"
        );

        public static Customer Jane => new
        (
            id: Guid.NewGuid(),
            firstName: "Jane",
            lastName: "Addison"
        );

        public static Customer NonExistingCustomer => new
        (
            id: Guid.NewGuid(),
            firstName: "MyNameIs",
            lastName: "ImpossibleToPersist"
        );
    }
}