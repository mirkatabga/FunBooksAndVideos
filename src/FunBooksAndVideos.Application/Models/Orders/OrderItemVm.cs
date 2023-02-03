namespace FunBooksAndVideos.Application.Models.Orders
{
    public record OrderItemVm(
        string Name,
        Guid OrderId,
        Guid? MembershipId,
        Guid? ProductId,
        int? Quantity);
}