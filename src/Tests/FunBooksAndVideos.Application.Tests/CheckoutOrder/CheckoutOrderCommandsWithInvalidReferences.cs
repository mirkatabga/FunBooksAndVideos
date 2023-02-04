using System.Collections;
using FunBooksAndVideos.Application.Contracts.Persistence;
using FunBooksAndVideos.Application.Features.Commands.Orders;
using FunBooksAndVideos.Application.Models.Orders;
using FunBooksAndVideos.Domain;

namespace FunBooksAndVideos.Application.Tests
{
    public class CheckoutOrderCommandsWithInvalidReferences : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new()
        {
            GetCommandOrderWithNonExistingCustomer(),
            GetCommandOrderWithNonExistingProduct(),
            GetCommandOrderWithNonExistingMembership()
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

        private static object[] GetCommandOrderWithNonExistingProduct()
        {
            var nonExistingProductId = Guid.NewGuid();
            var customerId = Guid.NewGuid();

            var command = new CheckoutOrderCommand(
                CustomerId: customerId,
                OrderItems: new List<OrderItemDto>
                {
                    new OrderItemDto
                    {
                         Name = "test1",
                         ProductId = nonExistingProductId,
                         Quantity = 1
                    }
                },
                DeliveryAddress: "address1");

            var uow = new Mock<IUnitOfWork>();

            uow.Setup(x => x.Customers.GetByIdAsync(customerId))
                .ReturnsAsync(new Customer(customerId, "John", "Doe"));

            uow.Setup(x => x.Products.GetByIds(new List<Guid> { nonExistingProductId }))
                .Returns(new List<Product>());

            return new object[] { command, uow.Object };
        }

        private static object[] GetCommandOrderWithNonExistingMembership()
        {
            var nonExistingMembershipId = Guid.NewGuid();
            var customerId = Guid.NewGuid();

            var command = new CheckoutOrderCommand(
                CustomerId: customerId,
                OrderItems: new List<OrderItemDto>
                {
                    new OrderItemDto
                    {
                         Name = "test1",
                         MembershipId = nonExistingMembershipId,
                    }
                });

            var uow = new Mock<IUnitOfWork>();

            uow.Setup(x => x.Products.GetByIds(new List<Guid>()))
                .Returns(new List<Product>());

            uow.Setup(x => x.Customers.GetByIdAsync(customerId))
                .ReturnsAsync(new Customer(customerId, "John", "Doe"));

            uow.Setup(x => x.Memberships.GetByIdAsync(nonExistingMembershipId))
                .ReturnsAsync(null as Membership);

            return new object[] { command, uow.Object };
        }
    }
}