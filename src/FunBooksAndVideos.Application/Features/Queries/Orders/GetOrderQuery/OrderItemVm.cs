namespace FunBooksAndVideos.Application.Features.Queries.Orders.GetOrderQuery
{
    public record OrderItemVm(
        string Name,
        Guid OrderId,
        Guid? MembershipId,
        Guid? ProductId,
        int? Quantity);
}