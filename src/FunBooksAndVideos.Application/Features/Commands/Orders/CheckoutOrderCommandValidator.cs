using FluentValidation;

namespace FunBooksAndVideos.Application.Features.Commands.Orders
{
    public class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
    {
        public CheckoutOrderCommandValidator()
        {
            RuleFor(p => p.CustomerId)
                .NotEmpty();

            RuleFor(p => p.OrderItems)
                .NotEmpty();
        }
    }
}