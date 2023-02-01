using FluentValidation;

namespace FunBooksAndVideos.Application.Features.Orders.Commands
{
    public class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
    {
        public CheckoutOrderCommandValidator()
        {
            RuleFor(p => p.CustomerId)
                .NotEmpty();

            RuleFor(p => p.OrderItems)
                .NotEmpty();

            RuleFor(p => p.TotalPrice)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}