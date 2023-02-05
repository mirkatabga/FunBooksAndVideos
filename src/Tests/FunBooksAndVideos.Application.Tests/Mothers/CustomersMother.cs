using FunBooksAndVideos.Domain;

namespace FunBooksAndVideos.Application.Tests.Mothers
{
    internal static class CustomersMother
    {
        public static Customer GetJohn() => new
        (
            id: Guid.NewGuid(),
            firstName: "John",
            lastName: "Doe"
        );

        public static Customer GetJane() => new
        (
            id: Guid.NewGuid(),
            firstName: "Jane",
            lastName: "Addison"
        );

        public static Customer GetNonExistingCustomer() => new
        (
            id: Guid.NewGuid(),
            firstName: "MyNameIs",
            lastName: "ImpossibleToPersist"
        );
    }
}