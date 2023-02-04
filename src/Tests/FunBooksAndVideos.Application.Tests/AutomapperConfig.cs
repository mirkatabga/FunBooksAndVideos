using AutoMapper;
using FunBooksAndVideos.Application.Features.Commands.Orders;

namespace FunBooksAndVideos.Application.Tests
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
        {
            var coreAssembly = typeof(CheckoutOrderCommandHandler).Assembly;

            return new MapperConfiguration(mc => mc.AddMaps(coreAssembly)).CreateMapper();
        }
    }
}