using System.Collections;
using FunBooksAndVideos.Application.Contracts.Persistence;
using Tests.FunBooksAndVideos.Application.Tests.Builders;
using static FunBooksAndVideos.Application.Tests.Mothers.CustomersMother;
using static FunBooksAndVideos.Application.Tests.Mothers.MembershipsMother;

namespace FunBooksAndVideos.Application.Tests
{
    public class InvalidCheckoutOrderCommands : IEnumerable<object[]>
    {
        private readonly CheckoutOrderCommandBuilder _commandBuilder = new();

        private List<object[]> Data => new()
        {
            GetCommandOrderWithMoreThenOneMembership(),
            GetCommandOrderWithSameMembershipAsCustomer()
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

            uow.Setup(x => x.Memberships.GetByIdAsync(bookClub.Id))
                .ReturnsAsync(bookClub);

            return new object[] { command, uow.Object };
        }
    }
}