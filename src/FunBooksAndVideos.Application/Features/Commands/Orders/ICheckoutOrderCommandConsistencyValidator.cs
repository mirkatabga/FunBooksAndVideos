namespace FunBooksAndVideos.Application.Features.Commands.Orders
{
    public interface ICheckoutOrderCommandConsistencyValidator
    {
        Task ValidateAsync(CheckoutOrderCommand request);
    }
}