using AutoMapper;
using FunBooksAndVideos.Application.Contracts.Persistence;
using FunBooksAndVideos.Application.Exceptions;
using FunBooksAndVideos.Application.Models.Orders;
using FunBooksAndVideos.Domain;
using MediatR;

namespace FunBooksAndVideos.Application.Features.Queries.Orders.GetOrderQuery
{
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OrderVm>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetOrderQueryHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _uow = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrderVm> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var order = await _uow.Orders.GetByIdAsync(request.OrderId, nameof(Order.OrderItems));

            if (order is null)
            {
                throw new NotFoundException(nameof(Order), request.OrderId);
            }

            return _mapper.Map<OrderVm>(order);
        }
    }
}