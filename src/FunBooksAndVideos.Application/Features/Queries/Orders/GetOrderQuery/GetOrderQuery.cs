using MediatR;

namespace FunBooksAndVideos.Application.Features.Queries.Orders.GetOrderQuery
{
    public record GetOrderQuery(Guid OrderId) : IRequest<OrderVm>;
}