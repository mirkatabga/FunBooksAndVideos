using System.Collections;
using FunBooksAndVideos.Application.Contracts.Persistence;
using FunBooksAndVideos.Domain;
using Tests.FunBooksAndVideos.Application.Tests.Builders;
using static FunBooksAndVideos.Application.Tests.Mothers.CustomersMother;
using static FunBooksAndVideos.Application.Tests.Mothers.MembershipsMother;
using static FunBooksAndVideos.Application.Tests.Mothers.ProductsMother;

namespace FunBooksAndVideos.Application.Tests
{
    public class CheckoutOrderCommandsWithInvalidReferences : IEnumerable<object[]>
    {
        private const string SESAME_STR = "Sesame street 1";
        private readonly CheckoutOrderCommandBuilder _commandBuilder = new();

        private List<object[]> Data => new()
        {
            GetCommandOrderWithNonExistingCustomer(),
            GetCommandOrderWithNonExistingProduct(),
            GetCommandOrderWithNonExistingMembership()
        };

        public IEnumerator<object[]> GetEnumerator() => Data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private object[] GetCommandOrderWithNonExistingCustomer()
        {
            var command = _commandBuilder
                .ForCustomer(NonExistingCustomer)
                .ForMembership(BookClub)
                .Build();

            var uow = new Mock<IUnitOfWork>();

            uow.Setup(x => x.Customers)
                .Returns(Mock.Of<ICustomerRepository>());

            uow.Setup(x => x.Memberships.GetByIdAsync(BookClub.Id))
                .ReturnsAsync(BookClub);

            return new object[] { command, uow.Object };
        }

        private object[] GetCommandOrderWithNonExistingProduct()
        {
            var command = _commandBuilder
                .ForCustomer(Jane)
                .ForPhysicalProducts(SESAME_STR, NonExistingPhysicalProduct)
                .Build();

            var uow = new Mock<IUnitOfWork>();

            uow.Setup(x => x.Customers.GetByIdAsync(Jane.Id))
                .ReturnsAsync(Jane);

            uow.Setup(x => x.Products.GetByIds(new List<Guid> { NonExistingPhysicalProduct.Id }))
                .Returns(new List<Product>());

            return new object[] { command, uow.Object };
        }

        private object[] GetCommandOrderWithNonExistingMembership()
        {
            var command = _commandBuilder
                .ForCustomer(John)
                .ForMembership(NonExistingMembership)
                .Build();

            var uow = new Mock<IUnitOfWork>();

            uow.Setup(x => x.Products.GetByIds(new List<Guid>()))
                .Returns(new List<Product>());

            uow.Setup(x => x.Customers.GetByIdAsync(John.Id))
                .ReturnsAsync(John);

            uow.Setup(x => x.Memberships.GetByIdAsync(NonExistingMembership.Id))
                .ReturnsAsync(null as Membership);

            return new object[] { command, uow.Object };
        }
    }
}