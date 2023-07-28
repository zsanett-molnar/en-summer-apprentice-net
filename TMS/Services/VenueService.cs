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

        public int Add(Venue venue)
        {
            throw new NotImplementedException();
        }

        public void Delete(Venue venue)
        {
            throw new NotImplementedException();
        }

        public Task<Venue> GetById(int id)
        {
            throw new NotImplementedException();
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
