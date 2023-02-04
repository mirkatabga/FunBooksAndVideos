using FunBooksAndVideos.Domain;

namespace FunBooksAndVideos.Application.Tests.Mothers
{
    internal static class MembershipsMother
    {
        public static Membership BookClub => new
        (
            id: Guid.NewGuid(),
            name: "Book Club",
            description: "New exciting book every month",
            price: 200
        );

        public static Membership VideoClub => new
        (
            id: Guid.NewGuid(),
            name: "Video Club",
            description: "New exciting video every week",
            price: 150
        );

        public static Membership Premium => new
        (
            id: Guid.NewGuid(),
            name: "Premium",
            description: "All benefits from book and video clubs",
            price: 300
        );

        public static Membership NonExistingMembership => new
        (
            id: Guid.NewGuid(),
            name: nameof(NonExistingMembership),
            description: "Non existing special offer",
            price: 0
        );
    }
}