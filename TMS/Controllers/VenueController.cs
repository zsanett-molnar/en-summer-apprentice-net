using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TMS.Models.Dto;
using TMS.Models;
using TMS.Repositories;
using Microsoft.IdentityModel.Tokens;
using TMS.Services;
using System;

namespace TMS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VenueController : Controller
    {
        private readonly IVenueService _venueService;

        public VenueController(IVenueService venueService, IVenueRepository venueRepository, IMapper mapper, ILogger<VenueController> logger)
        {
            _venueService = venueService;

        }

        [HttpGet]
        public ActionResult<List<VenueDto>> GetAll()
        {

            var venues = _venueService.GetAll();
            return Ok(venues);

        }

        [HttpGet]
        public async Task<ActionResult<VenueDto>> GetById(int id)
        {
            var @venue = await _venueService.GetById(id);
             if(@venue == null) 
             { 
                return NotFound();
             }
            return Ok(venue);
        }

        [HttpPatch]
        public async Task<ActionResult<VenueDto>> Patch(VenueDto venuePatch)
        {
            if (@venuePatch == null)
            {
                return NotFound();
            }
            _venueService.Update(@venuePatch);
            return NoContent();
        }


        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            _venueService.Delete(id);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> Add(VenueDto venuePost)
        {
            if (venuePost == null)
            {
                return NotFound();
            }
            return Ok(_venueService.Add(venuePost));
        }


    }
}
