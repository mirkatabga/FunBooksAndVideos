using FunBooksAndVideos.Application.Models;
using MediatR;

namespace FunBooksAndVideos.Application.Features.Commands.Orders
{
    public record CheckoutOrderCommand(
            Guid CustomerId,
            ICollection<OrderItemDto> OrderItems,
            string? DeliveryAddress = null) : IRequest<int>;
}