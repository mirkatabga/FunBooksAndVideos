using MediatR;

namespace FunBooksAndVideos.Application.Features.Events.Orders
{
    public record PhysicalProductsOrderedEvent(Guid OrderId) : INotification;
}