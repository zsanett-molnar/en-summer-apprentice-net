using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TMS.Models.Dto;
using TMS.Models;
using TMS.Repositories;

namespace TMS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VenueController : Controller
    {
        private readonly IVenueRepository _venueRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public VenueController(IVenueRepository venueRepository, IMapper mapper, ILogger<VenueController> logger)
        {
            _venueRepository = venueRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<List<Venue>> GetAll()
        {

            var venues = _venueRepository.GetAll();
            return Ok(venues);

        }
    }
}
