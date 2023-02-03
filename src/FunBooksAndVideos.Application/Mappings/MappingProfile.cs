using AutoMapper;
using FunBooksAndVideos.Application.Features.Queries.Orders.GetOrderQuery;
using FunBooksAndVideos.Domain;

namespace FunBooksAndVideos.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderVm>();
            CreateMap<OrderItem, OrderItemVm>();
        }
    }
}