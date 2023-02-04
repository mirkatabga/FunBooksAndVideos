using AutoMapper;
using FunBooksAndVideos.Application.Contracts.Persistence;
using FunBooksAndVideos.Application.Exceptions;
using FunBooksAndVideos.Application.Features.Commands.Orders;
using MediatR;

namespace FunBooksAndVideos.Application.Tests;

public class CheckoutOrderCommandHandlerTests
{
    private readonly IMapper _mapper = AutoMapperConfig.Initialize();

    [Theory]
    [ClassData(typeof(OrdersWithInvalidReferences))]
    public async Task Handle_OrderReferencingNotExistingEntities_ThrowsNotFoundException(CheckoutOrderCommand command, IUnitOfWork uow)
    {
        var handler = new CheckoutOrderCommandHandler(
            uow,
            _mapper,
            new CheckoutOrderCommandConsistencyValidator(uow),
            Mock.Of<IPublisher>());

        await handler
            .Invoking(h => h.Handle(command, CancellationToken.None))
            .Should()
            .ThrowExactlyAsync<NotFoundException>();
    }
}