using System.Collections;
using FunBooksAndVideos.Application.Models.Orders;

namespace FunBooksAndVideos.Application.Tests
{
    public class ValidationBehaviourOnCheckoutOrderCommand : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new()
        {
            new object[] { Guid.NewGuid(), null! },
            new object[] { Guid.NewGuid(), new List<OrderItemDto>() },
            new object[] { null!, new List<OrderItemDto> { new OrderItemDto() } },
            new object[] { Guid.Empty,  new List<OrderItemDto> { new OrderItemDto() } }
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}