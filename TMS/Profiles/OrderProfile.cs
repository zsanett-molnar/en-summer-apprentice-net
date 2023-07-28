using TMS.Models.Dto;
using TMS.Models;
using AutoMapper;

namespace TMS.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Order, OrderPatchDto>().ReverseMap();
            CreateMap<Order, OrderPostDto>().ReverseMap();
        }
    }   
}

