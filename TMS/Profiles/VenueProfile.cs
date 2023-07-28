using AutoMapper;
using TMS.Models;
using TMS.Models.Dto;

namespace TMS.Profiles
{
    public class VenueProfile : Profile
    {
        public VenueProfile()
        {
            CreateMap<Venue, VenueDto>().ReverseMap();
        }
    }
}
