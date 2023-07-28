using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TMS.Exceptions;
using TMS.Models;
using TMS.Models.Dto;

namespace TMS.Repositories
{
    public class VenueRepository : IVenueRepository
    {
        private readonly TicketManagementDbContext _dbContext;

        public VenueRepository()
        {
            _dbContext = new TicketManagementDbContext();
        }
        public int Add(Venue @venue)
        {
            _dbContext.Add(@venue);
            _dbContext.SaveChanges();
            return @venue.VenueId;
        }

        public void Delete(Venue @venue)
        {
            _dbContext.Remove(@venue);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Venue> GetAll()
        {
            var venues = _dbContext.Venues;

            return venues;
        }

        public async Task<Venue> GetById(int id)
        {
            var @venue = await _dbContext.Venues.Where(v => v.VenueId == id).FirstOrDefaultAsync();

            if (@venue == null)
                throw new EntityNotFoundException(id, nameof(Venue));

            return @venue;
        }

        public void Update(Venue @venue)
        {
            _dbContext.Entry(@venue).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
