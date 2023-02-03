using AutoMapper;
using FunBooksAndVideos.Application.Models.Orders;
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