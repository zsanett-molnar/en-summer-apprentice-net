using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;
using TMS.Models;
using TMS.Models.Dto;
using TMS.Repositories;

namespace TMS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EventController : ControllerBase

    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public EventController(IEventRepository eventRepository, IMapper mapper, ILogger<EventController> logger)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<List<EventDto>> GetAll()
        {

            var events = _eventRepository.GetAll();

            var dtoEvents = _mapper.Map<List<EventDto>>(events);

            return Ok(dtoEvents);

        }

        [HttpGet]
        public async Task<ActionResult<EventDto>> GetById(int id)
        {
            var @event = await _eventRepository.GetById(id);

            if(@event == null) 
            {
                return NotFound();
            }

            var eventDto = _mapper.Map<EventDto>(@event); 
            
            return Ok(eventDto);
        }


        [HttpPatch]
        public async Task<ActionResult<EventPatchDto>> Patch(EventPatchDto eventPatch)
        {
            if (eventPatch == null) throw new ArgumentNullException(nameof(eventPatch));
            var eventEntity = await _eventRepository.GetById(eventPatch.EventId);

            if(eventEntity == null)
            {
                return NotFound();
            }

            if(!eventPatch.EventName.IsNullOrEmpty()) eventEntity.Name = eventPatch.EventName;
            if (!eventPatch.EventDescription.IsNullOrEmpty()) eventEntity.Description = eventPatch.EventDescription;

            _eventRepository.Update(eventEntity);
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var eventEntity = await _eventRepository.GetById(id);
            if(eventEntity == null)
            {
                return NotFound();
            }
            _eventRepository.Delete(eventEntity);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> Add(EventPostDto eventPost)
        {
            if(eventPost == null)
            {
                return NotFound();
            }

            var newEvent = new Event();

            _mapper.Map(eventPost, newEvent);

            _eventRepository.Add(newEvent);
            return Ok(newEvent.EventId);
        }
    }
}
