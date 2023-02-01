using FunBooksAndVideos.Application.Contracts.Persistence;
using MediatR;

namespace FunBooksAndVideos.Application.Features.Commands.Orders
{
    public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
    {
        private readonly IUnitOfWork _uow;
        private readonly ICheckoutOrderCommandConsistencyValidator _consistencyValidator;

        public CheckoutOrderCommandHandler(
            IUnitOfWork unitOfWork,
            ICheckoutOrderCommandConsistencyValidator consistencyValidator)
        {
            _uow = unitOfWork;
            _consistencyValidator = consistencyValidator;
        }

        public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            await _consistencyValidator.ValidateAsync(request);

            return 0;
        }
    }
}