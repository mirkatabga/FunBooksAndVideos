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
            _customer = customer;

            return this;
        }

        public CheckoutOrderCommandBuilder ForPhysicalProducts(string? deliveryAddress, int? quantity, params PhysicalProduct[] physicalProducts)
        {
            _deliveryAddress = deliveryAddress;

            foreach (var product in physicalProducts)
            {
                _orderItems.Add(new OrderItemDto
                {
                    Name = product.Name,
                    MembershipId = null,
                    ProductId = product.Id,
                    Quantity = quantity
                });
            }

            return this;
        }

        public CheckoutOrderCommandBuilder ForDigitalProducts(params DigitalProduct[] digitalProducts)
        {
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