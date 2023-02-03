namespace FunBooksAndVideos.Application.Models.Orders
{
    public record OrderVm(
        Guid Id,
        decimal TotalPrice,
        Guid CustomerId,
        string? DeliveryAddress,
        ICollection<OrderItemVm> OrderItems);
}