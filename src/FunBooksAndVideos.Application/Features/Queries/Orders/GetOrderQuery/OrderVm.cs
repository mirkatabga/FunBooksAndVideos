namespace FunBooksAndVideos.Application.Features.Queries.Orders.GetOrderQuery
{
    public record OrderVm(
        decimal TotalPrice,
        Guid CustomerId,
        string? DeliveryAddress,
        ICollection<OrderItemVm> OrderItems);
}