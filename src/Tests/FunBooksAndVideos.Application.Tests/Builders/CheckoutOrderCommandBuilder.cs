using FunBooksAndVideos.Application.Features.Commands.Orders;
using FunBooksAndVideos.Application.Models.Orders;
using FunBooksAndVideos.Domain;

namespace Tests.FunBooksAndVideos.Application.Tests.Builders
{
    internal class CheckoutOrderCommandBuilder
    {
        private Customer? _customer;
        private List<OrderItemDto> _orderItems = new();
        private string? _deliveryAddress;

        public CheckoutOrderCommandBuilder ForCustomer(Customer customer)
        {
            if (customer is null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            _customer = customer;

            return this;
        }

        public CheckoutOrderCommandBuilder ForPhysicalProducts(string? deliveryAddress, params PhysicalProduct[] physicalProducts)
        {
            if (physicalProducts.Length == 0)
            {
                throw new ArgumentException($"{nameof(physicalProducts)} should contain at least one element.");
            }

            _deliveryAddress = deliveryAddress;

            foreach (var product in physicalProducts)
            {
                _orderItems.Add(new OrderItemDto
                {
                    Name = product.Name,
                    MembershipId = null,
                    ProductId = product.Id,
                    Quantity = product.Quantity
                });
            }

            return this;
        }

        public CheckoutOrderCommandBuilder ForDigitalProducts(params DigitalProduct[] digitalProducts)
        {
            if (digitalProducts.Length == 0)
            {
                throw new ArgumentException($"{nameof(digitalProducts)} should contain at least one element.");
            }

            foreach (var product in digitalProducts)
            {
                _orderItems.Add(new OrderItemDto
                {
                    Name = product.Name,
                    MembershipId = null,
                    ProductId = product.Id,
                    Quantity = null
                });
            }

            return this;
        }

        public CheckoutOrderCommandBuilder ForMembership(Membership membership)
        {
            if (membership is null)
            {
                throw new ArgumentNullException(nameof(membership));
            }

            _orderItems.Add(new OrderItemDto
            {
                Name = membership.Name,
                MembershipId = membership.Id,
                ProductId = null,
                Quantity = null
            });

            return this;
        }

        public CheckoutOrderCommand Build()
        {
            var command = new CheckoutOrderCommand(
                CustomerId: _customer!.Id,
                OrderItems: _orderItems,
                DeliveryAddress: _deliveryAddress
            );

            Reset();

            return command;
        }

        private void Reset()
        {
            _customer = null;
            _deliveryAddress = null;
            _orderItems = new List<OrderItemDto>();
        }
    }
}