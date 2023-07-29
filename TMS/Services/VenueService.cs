using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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


        public async void Delete(int id)
        {
            var venueEntity = await _venueRepository.GetById(id);
            _venueRepository.Delete(venueEntity);
        }

        public async Task<VenueDto> GetById(int id)
        {
            var @venue = await _venueRepository.GetById(id);
            var venueDto = _mapper.Map<VenueDto>(@venue);
            return venueDto;

        }

        public async void Update(VenueDto venuePatch)
        {
            if (venuePatch == null) throw new ArgumentNullException(nameof(venuePatch));
            var venueEntity = await _venueRepository.GetById(venuePatch.VenueId);


            if (!venuePatch.Location.IsNullOrEmpty()) venueEntity.Location = venuePatch.Location;
            if (!venuePatch.Type.IsNullOrEmpty()) venueEntity.Type = venuePatch.Type;
            if (venuePatch.Capacity >= 0) venueEntity.Capacity = venuePatch.Capacity;
            _venueRepository.Update(venueEntity);
        }

        IEnumerable<VenueDto> IVenueService.GetAll()
        {
            var venues = _venueRepository.GetAll();
            var venuesDto = _mapper.Map<List<VenueDto>>(venues);
            return venuesDto;
        }
    }
}
