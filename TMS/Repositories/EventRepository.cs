using Microsoft.EntityFrameworkCore;
using TMS.Exceptions;
using TMS.Models;

namespace TMS.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly TicketManagementDbContext _dbContext;

        public EventRepository()
        {
            _dbContext = new TicketManagementDbContext();
        }

        public int Add(Event @event)
        {
            _dbContext.Add(@event);
            _dbContext.SaveChanges();
            return @event.EventId;
        }

        public void Delete(Event @event)
        {
            _dbContext.Remove(@event);
            _dbContext.SaveChanges();
          
        }

        public IEnumerable<Event> GetAll()
        {
            var events = _dbContext.Events;

            return events;
        }

        public async Task<Event> GetById(int id)
        {
            //var @event = _dbContext.Events.Where(e => e.EventId == id).FirstOrDefault();
            var @event = await _dbContext.Events.Where(e => e.EventId == id).FirstOrDefaultAsync();

            if (@event == null)
                throw new EntityNotFoundException(id, nameof(Event));

            return @event;
        }

        public void Update(Event @event)
        {
            _dbContext.Entry(@event).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }

}

