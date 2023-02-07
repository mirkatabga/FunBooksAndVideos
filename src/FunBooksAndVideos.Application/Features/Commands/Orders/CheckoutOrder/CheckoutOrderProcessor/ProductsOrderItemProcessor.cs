using FunBooksAndVideos.Application.Contracts.Persistence;
using FunBooksAndVideos.Application.Models.Orders;
using FunBooksAndVideos.Domain;

namespace FunBooksAndVideos.Application.Features.Commands.Orders.CheckoutOrder.CheckoutOrderProcessor
{
    public class ProductsOrderItemProcessor : IOrderItemsProcessor
    {
        private readonly IUnitOfWork _uow;

        public ProductsOrderItemProcessor(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }

        public async Task<ProcessOrderResponse> ProcessAsync(CheckoutOrderCommand command, ProcessOrderResponse response)
        {
            var productIds = command.GetProductIdsForOrder();

            if (productIds.Count == 0)
            {
                return response;
            }

            var customer = await _uow.Customers.GetByIdAsync(command.CustomerId, nameof(Customer.Products));
            var products = await _uow.Products.GetByIdsAsync(productIds);

            AddMissingProductsToCustomer(customer, products);

            foreach (var item in command.OrderItems)
            {
                if (item.ProductId is null)
                {
                    continue;
                }

                var product = products.Single(p => p.Id == item.ProductId);

                if (!response.HasPhysicalProduct && product is PhysicalProduct)
                {
                    response.HasPhysicalProduct = true;
                }

                response.Order.AddOrderItem(new OrderItem(
                    id: Guid.NewGuid(),
                    name: product.Name,
                    membershipId: null,
                    productId: product.Id,
                    quantity: item.Quantity ?? 1,
                    price: product.Price));
            }

            return response;
        }

        private static void AddMissingProductsToCustomer(Customer? customer, IEnumerable<Product> products)
        {
            var productsToAdd = new HashSet<Product>();

            foreach (var product in products)
            {
                if (customer!.Products.Any(p => p.Id == product.Id))
                {
                    continue;
                }

                productsToAdd.Add(product);
            }

            customer!.AddProducts(productsToAdd);
        }
    }
}