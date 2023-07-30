using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;
using TMS.Models;
using TMS.Models.Dto;
using TMS.Repositories;
using TMS.Services;

namespace TMS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EventController : ControllerBase

    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
            
        }

        [HttpGet]
        public ActionResult<List<EventDto>> GetAll()
        {
            return Ok(_eventService.GetAll());

        }

        [HttpGet]
        public async Task<ActionResult<EventDto>> GetById(int id)
        {
           var eventDto = await _eventService.GetById(id);
            if(eventDto == null)
            {
                return NotFound();
            }

            return Ok(eventDto);
        }


        [HttpPatch]
        public async Task<ActionResult<EventPatchDto>> Patch(EventPatchDto eventPatch)
        {
            if (eventPatch == null) throw new ArgumentNullException(nameof(eventPatch));
            _eventService.Update(eventPatch);
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            _eventService.Delete(id);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> Add(EventPostDto eventPost)
        {
            var newEvent = _eventService.Add(eventPost);
            return Ok(newEvent);
        }
    }
 }

