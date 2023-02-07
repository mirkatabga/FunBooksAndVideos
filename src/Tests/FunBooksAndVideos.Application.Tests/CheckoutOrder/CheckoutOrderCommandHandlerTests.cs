using AutoMapper;
using FluentValidation;
using FunBooksAndVideos.Application.Behaviours;
using FunBooksAndVideos.Application.Contracts.Persistence;
using FunBooksAndVideos.Application.Exceptions;
using FunBooksAndVideos.Application.Features.Commands.Orders;
using FunBooksAndVideos.Application.Models.Orders;
using MediatR;
using FunBooksAndVideos.Domain;
using FunBooksAndVideos.Application.Features.Events.Orders;
using Tests.FunBooksAndVideos.Application.Tests.Builders;
using static FunBooksAndVideos.Application.Tests.Mothers.CustomersMother;
using static FunBooksAndVideos.Application.Tests.Mothers.ProductsMother;
using FunBooksAndVideos.Application.Features.Commands.Orders.CheckoutOrder.CheckoutOrderProcessor;

namespace FunBooksAndVideos.Application.Tests;

public class CheckoutOrderCommandHandlerTests
{
    private const string RAINBOW_STREET_1 = "Rainbow str 1";
    private readonly IMapper _mapper = AutoMapperConfig.Initialize();
    private readonly CheckoutOrderCommandBuilder _commandBuilder = new();

    [Theory]
    [ClassData(typeof(CheckoutOrderCommandsWithInvalidReferences))]
    public async Task Handle_OrderReferencingNotExistingEntities_ThrowsNotFoundException(CheckoutOrderCommand command, IUnitOfWork uow)
    {
        var handler = new CheckoutOrderCommandHandler(
            _mapper,
            new CheckoutOrderCommandConsistencyValidator(uow),
            Mock.Of<IPublisher>(),
            new OrderProcessor(uow, GetProcessors(uow)));

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
            _mapper,
            new CheckoutOrderCommandConsistencyValidator(uow),
            Mock.Of<IPublisher>(),
            new OrderProcessor(uow, GetProcessors(uow)));

        await validationBehavior.Invoking(x => x.Handle(
            command,
            () => checkoutOrderCommandHandler.Handle(command, CancellationToken.None),
            CancellationToken.None))
            .Should()
            .ThrowExactlyAsync<Exceptions.ValidationException>();
    }

    [Theory]
    [ClassData(typeof(InvalidCheckoutOrderCommands))]
    public async Task Handle_InvalidCheckoutOrderCommand_ShouldThrowValidationException(
            CheckoutOrderCommand command, IUnitOfWork uow)
    {
        var handler = new CheckoutOrderCommandHandler(
            _mapper,
            new CheckoutOrderCommandConsistencyValidator(uow),
            Mock.Of<IPublisher>(),
            new OrderProcessor(uow, GetProcessors(uow)));

        await handler.Invoking(h => h.Handle(command, CancellationToken.None))
            .Should()
            .ThrowExactlyAsync<Exceptions.ValidationException>();
    }

    [Fact]
    public async Task Handle_CheckoutOrderCommandWithPhysicalProduct_ShouldPublishPhysicalProductsOrderedEvent()
    {
        var jane = GetJane();
        var lotrHardCover = GetLOTRHardCover();

        var command = _commandBuilder
            .ForCustomer(jane)
            .ForPhysicalProducts(RAINBOW_STREET_1, 1, lotrHardCover)
            .Build();

        var uow = new Mock<IUnitOfWork>();

        uow.Setup(x => x.Customers.GetByIdAsync(jane.Id, It.IsAny<string[]>()))
            .ReturnsAsync(jane);

        uow.Setup(x => x.Products.GetByIdsAsync(It.Is<IEnumerable<Guid>>(
                productIds => productIds.SequenceEqual(new List<Guid> { lotrHardCover.Id }))))
            .ReturnsAsync(new List<Product> { lotrHardCover });

        uow.Setup(x => x.Memberships)
            .Returns(Mock.Of<IMembershipRepository>());

        uow.Setup(x => x.Orders)
            .Returns(Mock.Of<IOrderRepository>());

        var publisher = new Mock<IPublisher>();

        var commandHandler = new CheckoutOrderCommandHandler(
            _mapper,
            new CheckoutOrderCommandConsistencyValidator(uow.Object),
            publisher.Object,
            new OrderProcessor(uow.Object, GetProcessors(uow.Object)));

        var orderVm = await commandHandler.Handle(command, CancellationToken.None);

        publisher.Verify(
            expression: p => p.Publish(new PhysicalProductsOrderedEvent(orderVm.Id), It.IsAny<CancellationToken>()),
            times: Times.Once);
    }
    private ICollection<IOrderItemsProcessor> GetProcessors(IUnitOfWork uow) => new List<IOrderItemsProcessor>
    {
        new MembershipOrderItemProcessor(uow),
        new ProductsOrderItemProcessor(uow)
    };
}