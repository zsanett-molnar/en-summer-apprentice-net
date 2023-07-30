using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using TMS.Models;
using TMS.Models.Dto;
using TMS.Repositories;

namespace TMS.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public EventService(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public Event Add(EventPostDto @event)
        {
            var newEvent  = new Event();

            _mapper.Map(@event, newEvent);

            _eventRepository.Add(newEvent);

            return newEvent;
        }

        public async void Delete(int id)
        {
            var eventEntity = await _eventRepository.GetById(id);
            _eventRepository.Delete(eventEntity);
        }

        public IEnumerable<EventDto> GetAll()
        {
            var events = _eventRepository.GetAll();
            var eventsDto = _mapper.Map<List<EventDto>>(events);
            return eventsDto;
        }

        public async Task<EventDto> GetById(int id)
        {
            var @event = await _eventRepository.GetById(id);
            var eventDto = _mapper.Map<EventDto>(@event);
            return eventDto;
        }

        public async void Update(EventPatchDto eventPatch)
        {
            if (eventPatch == null) throw new ArgumentNullException(nameof(eventPatch));
            var eventEntity = await _eventRepository.GetById(eventPatch.EventId);


            if (!eventPatch.EventName.IsNullOrEmpty()) eventEntity.Name = eventPatch.EventName;
            if (!eventPatch.EventDescription.IsNullOrEmpty()) eventEntity.Description = eventPatch.EventDescription;

            _eventRepository.Update(eventEntity);
        }
    }
}
