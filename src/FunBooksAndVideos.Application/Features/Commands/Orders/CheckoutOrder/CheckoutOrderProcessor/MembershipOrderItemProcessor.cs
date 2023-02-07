using FunBooksAndVideos.Application.Contracts.Persistence;
using FunBooksAndVideos.Application.Models.Orders;
using FunBooksAndVideos.Domain;

namespace FunBooksAndVideos.Application.Features.Commands.Orders.CheckoutOrder.CheckoutOrderProcessor
{

    public class MembershipOrderItemProcessor : IOrderItemsProcessor
    {
        private readonly IUnitOfWork _uow;

        public MembershipOrderItemProcessor(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }

        public async Task<ProcessOrderResponse> ProcessAsync(CheckoutOrderCommand command, ProcessOrderResponse response)
        {
            var customer = await _uow.Customers.GetByIdAsync(command.CustomerId);
            var membershipIds = command.GetMembershipsIdsForOrder();

            if (!membershipIds.Any())
            {
                return response;
            }

            var membership = await _uow.Memberships.GetByIdAsync(membershipIds.Single());

            customer!.UpdateMembership(membership);

            response.Order.AddOrderItem(new OrderItem(
                id: Guid.NewGuid(),
                name: membership!.Name,
                membershipId: membership!.Id,
                productId: null,
                quantity: 1,
                price: membership.Price));

            return response;
        }
    }
}