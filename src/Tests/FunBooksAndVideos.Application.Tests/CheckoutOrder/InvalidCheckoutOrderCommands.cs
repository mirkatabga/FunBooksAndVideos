using System.Collections;
using FunBooksAndVideos.Application.Contracts.Persistence;
using FunBooksAndVideos.Application.Models.Orders;
using FunBooksAndVideos.Domain;
using Tests.FunBooksAndVideos.Application.Tests.Builders;
using static FunBooksAndVideos.Application.Tests.Mothers.CustomersMother;
using static FunBooksAndVideos.Application.Tests.Mothers.MembershipsMother;
using static FunBooksAndVideos.Application.Tests.Mothers.ProductsMother;

namespace FunBooksAndVideos.Application.Tests
{
    public class InvalidCheckoutOrderCommands : IEnumerable<object[]>
    {
        private const string SESAME_STR = "Sesame street 1";

        private readonly CheckoutOrderCommandBuilder _commandBuilder = new();

        private List<object[]> Data => new()
        {
            GetCommandOrderWithMoreThenOneMembership(),
            GetCommandOrderWithSameMembershipAsCustomer(),
            GetCommandWithNullOrderItemDto(),
            GetCommandWithOrderItemWithoutPurchases(),
            GetCommandWithOrderItemContainingMembershipAndProduct(),
            GetCommandWithInvalidQuantity(null!),
            GetCommandWithInvalidQuantity(0),
            GetCommandWithInvalidQuantity(int.MaxValue)
        };

        public IEnumerator<object[]> GetEnumerator() => Data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private object[] GetCommandOrderWithMoreThenOneMembership()
        {
            var john = GetJohn();
            var bookClub = GetBookClub();
            var videoClub = GetVideoClub();

            var command = _commandBuilder
                .ForCustomer(john)
                .ForMembership(bookClub)
                .ForMembership(videoClub)
                .Build();

            var uow = new Mock<IUnitOfWork>();

            uow.Setup(x => x.Customers.GetByIdAsync(john.Id))
                .ReturnsAsync(john);

            uow.Setup(x => x.Products)
                .Returns(Mock.Of<IProductRepository>());

            return new object[] { command, uow.Object };
        }

        private object[] GetCommandOrderWithSameMembershipAsCustomer()
        {
            var john = GetJohn();
            var bookClub = GetBookClub();
            john.UpdateMembership(bookClub);

            var command = _commandBuilder
                .ForCustomer(john)
                .ForMembership(bookClub)
                .Build();

            var uow = new Mock<IUnitOfWork>();

            uow.Setup(x => x.Customers.GetByIdAsync(john.Id))
                .ReturnsAsync(john);

            uow.Setup(x => x.Products)
                .Returns(Mock.Of<IProductRepository>());

            uow.Setup(x => x.Memberships.GetByIdAsync(bookClub.Id))
                .ReturnsAsync(bookClub);

            return new object[] { command, uow.Object };
        }

        private object[] GetCommandWithNullOrderItemDto()
        {
            return GetCommandWithOrderItemDto(null);
        }

        private object[] GetCommandWithOrderItemWithoutPurchases()
        {
            return GetCommandWithOrderItemDto(new OrderItemDto
            {
                Name = "OrderItemWithoutPurchases",
                MembershipId = null,
                ProductId = null
            });
        }

        private object[] GetCommandWithOrderItemContainingMembershipAndProduct()
        {
            var john = GetJohn();
            var lotrHardCover = GetLOTRHardCover();
            var premium = GetPremium();

            var command = _commandBuilder
                .ForCustomer(john)
                .Build();

            command.OrderItems.Add(new OrderItemDto
            {
                Name = "ContainsBothMembershipAndProduct",
                MembershipId = premium.Id,
                ProductId = lotrHardCover.Id,
                Quantity = 2
            });

            var uow = new Mock<IUnitOfWork>();

            uow.Setup(x => x.Customers.GetByIdAsync(john.Id))
                .ReturnsAsync(john);

            uow.Setup(x => x.Products.GetByIds(It.IsAny<IEnumerable<Guid>>()))
                .Returns(new List<Product> { lotrHardCover });

            uow.Setup(x => x.Memberships.GetByIdAsync(premium.Id))
                .ReturnsAsync(premium);

            return new object[] { command, uow.Object };
        }

        private object[] GetCommandWithInvalidQuantity(int? quantity)
        {
            var jane = GetJane();
            var lotrHardCover = GetLOTRHardCover();

            var command = _commandBuilder
                .ForCustomer(jane)
                .ForPhysicalProducts(SESAME_STR, quantity, lotrHardCover)
                .Build();

            var uow = new Mock<IUnitOfWork>();

            uow.Setup(x => x.Customers.GetByIdAsync(jane.Id))
                .ReturnsAsync(jane);

            uow.Setup(x => x.Products.GetByIds(It.IsAny<IEnumerable<Guid>>()))
                .Returns(new List<Product> { lotrHardCover });

            uow.Setup(x => x.Memberships)
                .Returns(Mock.Of<IMembershipRepository>());

            return new object[] { command, uow.Object };
        }

        private object[] GetCommandWithOrderItemDto(OrderItemDto? orderItem)
        {
            var jane = GetJane();

            var command = _commandBuilder
                .ForCustomer(jane)
                .Build();

            command.OrderItems.Add(orderItem!);

            var uow = new Mock<IUnitOfWork>();

            uow.Setup(x => x.Customers.GetByIdAsync(jane.Id))
                .ReturnsAsync(jane);

            uow.Setup(x => x.Products)
                .Returns(Mock.Of<IProductRepository>());

            uow.Setup(x => x.Memberships)
                .Returns(Mock.Of<IMembershipRepository>());

            return new object[] { command, uow.Object };
        }
    }
}