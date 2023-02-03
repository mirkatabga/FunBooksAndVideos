using FunBooksAndVideos.Application.Models.Orders;
using MediatR;

namespace FunBooksAndVideos.Application.Features.Queries.Orders.GetOrderQuery
{
    public record GetOrderQuery(Guid OrderId) : IRequest<OrderVm>;
}