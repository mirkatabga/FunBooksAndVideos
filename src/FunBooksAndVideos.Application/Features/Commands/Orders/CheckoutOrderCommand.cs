using FunBooksAndVideos.Application.Models;
using MediatR;

namespace FunBooksAndVideos.Application.Features.Commands.Orders
{
    public record CheckoutOrderCommand(
            Guid CustomerId,
            ICollection<OrderItemDto> OrderItems,
            string? DeliveryAddress = null) : IRequest<Guid>
    {
        public ICollection<Guid> GetMembershipsIdsForOrder()
        {
            return OrderItems
                .Where(item => item.MembershipId is not null)
                .Select(item => item.MembershipId!.Value)
                .Distinct()
                .ToList();
        }

        public ICollection<Guid> GetProductIdsForOrder()
        {
            return OrderItems
                 .Where(item => item.ProductId is not null)
                 .Select(item => item.ProductId!.Value)
                 .Distinct()
                 .ToList();
        }
    }
}