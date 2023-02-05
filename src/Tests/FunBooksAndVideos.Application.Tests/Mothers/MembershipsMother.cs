using FunBooksAndVideos.Domain;

namespace FunBooksAndVideos.Application.Tests.Mothers
{
    internal static class MembershipsMother
    {
        public static Membership GetBookClub() => new
        (
            id: Guid.NewGuid(),
            name: "Book Club",
            description: "New exciting book every month",
            price: 200
        );

        public static Membership GetVideoClub() => new
        (
            id: Guid.NewGuid(),
            name: "Video Club",
            description: "New exciting video every week",
            price: 150
        );

        public static Membership GetPremium() => new
        (
            id: Guid.NewGuid(),
            name: "Premium",
            description: "All benefits from book and video clubs",
            price: 300
        );

        public static Membership GetNonExistingMembership() => new
        (
            id: Guid.NewGuid(),
            name: "NonExistingMembership",
            description: "Non existing special offer",
            price: 0
        );
    }
}