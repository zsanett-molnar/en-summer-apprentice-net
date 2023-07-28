using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TMS.Models.Dto;
using TMS.Models;
using TMS.Repositories;
using Microsoft.IdentityModel.Tokens;
using TMS.Services;

namespace TMS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VenueController : Controller
    {
        private readonly IVenueService _venueService;
        private readonly IVenueRepository _venueRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public VenueController(IVenueService venueService, IVenueRepository venueRepository, IMapper mapper, ILogger<VenueController> logger)
        {
            _venueService = venueService;
            _venueRepository = venueRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<List<VenueDto>> GetAll()
        {

            /*var venues = _venueRepository.GetAll();
            var venueDto = _mapper.Map<List<VenueDto>>(venues);
            return Ok(venueDto);*/

            var venues = _venueService.GetAll();
            var venuesDto = _mapper.Map<List<VenueDto>>(venues);
            return Ok(venuesDto);

        }

        [HttpGet]
        public async Task<ActionResult<VenueDto>> GetById(int id)
        {
            var @venue = await _venueRepository.GetById(id);

            if (@venue == null)
            {
                return NotFound();
            }

            var venueDto = _mapper.Map<VenueDto>(@venue);
            return Ok(venueDto);
        }

        [HttpPatch]
        public async Task<ActionResult<VenueDto>> Patch(VenueDto venuePatch)
        {
            if (venuePatch == null) throw new ArgumentNullException(nameof(venuePatch));
            var venueEntity = await _venueRepository.GetById(venuePatch.VenueId);

            if (venueEntity == null)
            {
                return NotFound();
            }

            if (!venuePatch.Location.IsNullOrEmpty()) venueEntity.Location = venueEntity.Location;
            if (!venuePatch.Type.IsNullOrEmpty()) venueEntity.Type = venueEntity.Type;
            if (venuePatch.Capacity >= 0) venueEntity.Capacity = venuePatch.Capacity;
            _venueRepository.Update(venueEntity);
            return NoContent();
        }


        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var venueEntity = await _venueRepository.GetById(id);
            if (venueEntity == null)
            {
                return NotFound();
            }
            _venueRepository.Delete(venueEntity);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> Add(VenueDto venuePost)
        {
            if (venuePost == null)
            {
                return NotFound();
            }

            var newVenue = new Venue();

            _mapper.Map(venuePost, newVenue);

            _venueRepository.Add(newVenue);
            return Ok(newVenue.VenueId);
        }


    }
}
