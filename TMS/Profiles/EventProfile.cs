using AutoMapper;
using TMS.Models;
using TMS.Models.Dto;

namespace TMS.Profiles
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Event, EventDto>().ReverseMap();
            CreateMap<Event, EventPatchDto>().ReverseMap();
            CreateMap<Event, EventPostDto>().ReverseMap();
        }
    }
}
