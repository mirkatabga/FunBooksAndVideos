using FunBooksAndVideos.Application.Models;
using MediatR;

namespace FunBooksAndVideos.Application.Features.Orders.Commands
{
    public record CheckoutOrderCommand(
            Guid CustomerId,
            decimal TotalPrice,
            ICollection<OrderItemDto> OrderItems,
            string? DeliveryAddress = null) : IRequest<int>;
}