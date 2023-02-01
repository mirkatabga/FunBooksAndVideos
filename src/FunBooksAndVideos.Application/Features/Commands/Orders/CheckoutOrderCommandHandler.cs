using FunBooksAndVideos.Application.Contracts.Persistence;
using FunBooksAndVideos.Application.Exceptions;
using FunBooksAndVideos.Domain;
using FunBooksAndVideos.Domain.Common;
using MediatR;

namespace FunBooksAndVideos.Application.Features.Commands.Orders
{
    public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
    {
        private readonly IUnitOfWork _uow;

        public CheckoutOrderCommandHandler(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }

        public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var customer = await _uow.Customers.GetByIdAsync(request.CustomerId);

            if (customer is null)
            {
                throw new NotFoundException(nameof(Customer), request.CustomerId);
            }

            return 0;
        }
    }
}