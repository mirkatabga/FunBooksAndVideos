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
            var nonExistingCustomer = GetNonExistingCustomer();
            var bookClub = GetBookClub();

            var command = _commandBuilder
                .ForCustomer(nonExistingCustomer)
                .ForMembership(bookClub)
                .Build();

            var uow = new Mock<IUnitOfWork>();

            uow.Setup(x => x.Customers)
                .Returns(Mock.Of<ICustomerRepository>());

            uow.Setup(x => x.Memberships.GetByIdAsync(bookClub.Id))
                .ReturnsAsync(bookClub);

            return new object[] { command, uow.Object };
        }

        private object[] GetCommandOrderWithNonExistingProduct()
        {
            var jane = GetJane();
            var nonExistingProduct = GetNonExistingPhysicalProduct();

            var command = _commandBuilder
                .ForCustomer(jane)
                .ForPhysicalProducts(SESAME_STR, nonExistingProduct)
                .Build();

            var uow = new Mock<IUnitOfWork>();

            uow.Setup(x => x.Customers.GetByIdAsync(jane.Id))
                .ReturnsAsync(jane);

            uow.Setup(x => x.Products.GetByIds(new List<Guid> { nonExistingProduct.Id }))
                .Returns(new List<Product>());

            return new object[] { command, uow.Object };
        }

        private object[] GetCommandOrderWithNonExistingMembership()
        {
            var john = GetJohn();
            var nonExistingMembership = GetNonExistingMembership();

            var command = _commandBuilder
                .ForCustomer(john)
                .ForMembership(nonExistingMembership)
                .Build();

            var uow = new Mock<IUnitOfWork>();

            uow.Setup(x => x.Products.GetByIds(new List<Guid>()))
                .Returns(new List<Product>());

            uow.Setup(x => x.Customers.GetByIdAsync(john.Id))
                .ReturnsAsync(john);

            uow.Setup(x => x.Memberships.GetByIdAsync(nonExistingMembership.Id))
                .ReturnsAsync(null as Membership);

            return new object[] { command, uow.Object };
        }
    }
}