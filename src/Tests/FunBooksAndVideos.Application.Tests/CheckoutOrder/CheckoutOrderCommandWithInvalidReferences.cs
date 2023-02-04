using System.Collections;
using FunBooksAndVideos.Application.Contracts.Persistence;
using FunBooksAndVideos.Application.Features.Commands.Orders;
using FunBooksAndVideos.Application.Models.Orders;
using FunBooksAndVideos.Domain;

namespace FunBooksAndVideos.Application.Tests
{
    public class OrdersWithInvalidReferences : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new()
        {
            GetCommandOrderWithNonExistingCustomer()
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private static object[] GetCommandOrderWithNonExistingCustomer()
        {
            var nonExistingCustomerId = Guid.NewGuid();
            var membership = new Membership(Guid.NewGuid(), "membership1", "desc1", 100);

            var command = new CheckoutOrderCommand(
                nonExistingCustomerId,
                new List<OrderItemDto>
            {
                new OrderItemDto { Name = "test1", MembershipId = membership.Id }
            });

            var uow = new Mock<IUnitOfWork>();

            uow
                .Setup(x => x.Customers)
                .Returns(Mock.Of<ICustomerRepository>());

            uow
                .Setup(x => x.Memberships.GetByIdAsync(membership.Id))
                .ReturnsAsync(membership);

            return new object[] { command, uow.Object };
        }
    }
}