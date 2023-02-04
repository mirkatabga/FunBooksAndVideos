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
        };

        public IEnumerator<object[]> GetEnumerator() => Data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private object[] GetCommandOrderWithMoreThenOneMembership()
        {
            var command = _commandBuilder
                .ForCustomer(John)
                .ForMembership(BookClub)
                .ForMembership(VideoClub)
                .Build();

            var uow = new Mock<IUnitOfWork>();

            uow.Setup(x => x.Customers.GetByIdAsync(John.Id))
                .ReturnsAsync(John);

            return new object[] { command, uow.Object };
        }
    }
}