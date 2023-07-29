using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TMS.Models;
using TMS.Models.Dto;
using TMS.Repositories;

namespace TMS.Services
{
    public class VenueService : IVenueService
    {
        private readonly IVenueRepository _venueRepository;
        private readonly IMapper _mapper;

        public VenueService(IVenueRepository venueRepository, IMapper mapper)
        {
            _venueRepository = venueRepository;
            _mapper = mapper;
        }

        public int Add(VenueDto venue)
        {
 
            var newVenue = new Venue();

            _mapper.Map(venue, newVenue);

            _venueRepository.Add(newVenue);

            return newVenue.VenueId;
        }


        public void Delete(Venue venue)
        {
            throw new NotImplementedException();
        }

        public async Task<VenueDto> GetById(int id)
        {
            var @venue = await _venueRepository.GetById(id);
            var venueDto = _mapper.Map<VenueDto>(@venue);
            return venueDto;

        }

        public void Update(Venue venue)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Venue> IVenueService.GetAll()
        {
            var venues = _venueRepository.GetAll();
            return venues;
        }
    }
}
