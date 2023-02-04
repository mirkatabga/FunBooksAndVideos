using AutoMapper;
using FluentValidation;
using FunBooksAndVideos.Application.Behaviours;
using FunBooksAndVideos.Application.Contracts.Persistence;
using FunBooksAndVideos.Application.Exceptions;
using FunBooksAndVideos.Application.Features.Commands.Orders;
using FunBooksAndVideos.Application.Models.Orders;
using MediatR;

namespace FunBooksAndVideos.Application.Tests;

public class CheckoutOrderCommandHandlerTests
{
    private readonly IMapper _mapper = AutoMapperConfig.Initialize();

    [Theory]
    [ClassData(typeof(CheckoutOrderCommandsWithInvalidReferences))]
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

    [Theory]
    [ClassData(typeof(ValidationBehaviourOnCheckoutOrderCommand))]
    public async Task Handle_ValidationBehaviourOnCheckoutOrderCommand_ShouldThrowValidaitonException(
        Guid customerId,
        ICollection<OrderItemDto>? orderItems)
    {
        var validators = new List<IValidator<CheckoutOrderCommand>>()
        {
            new CheckoutOrderCommandValidator()
        };

        var validationBehavior = new ValidationBehaviour<CheckoutOrderCommand, OrderVm>(validators);

        var command = new CheckoutOrderCommand(
            CustomerId: customerId,
            OrderItems: orderItems!
        );

        var uow = Mock.Of<IUnitOfWork>();

        var checkoutOrderCommandHandler = new CheckoutOrderCommandHandler(
            uow,
            _mapper,
            new CheckoutOrderCommandConsistencyValidator(uow),
            Mock.Of<IPublisher>());

        await validationBehavior.Invoking(x => x.Handle(
            command,
            () => checkoutOrderCommandHandler.Handle(command, CancellationToken.None),
            CancellationToken.None))
            .Should()
            .ThrowExactlyAsync<Exceptions.ValidationException>();
    }

    [Theory]
    [ClassData(typeof(InvalidCheckoutOrderCommands))]
    public void Handle_CheckoutOrderCommandHandler_ShouldThrowValidationException(
            CheckoutOrderCommand command, IUnitOfWork uow)
    {
        var handler = new CheckoutOrderCommandHandler(
            uow,
            _mapper,
            new CheckoutOrderCommandConsistencyValidator(uow),
            Mock.Of<IPublisher>());

        handler.Invoking(h => h.Handle(command, CancellationToken.None))
            .Should()
            .ThrowExactlyAsync<Exceptions.ValidationException>();
    }
}