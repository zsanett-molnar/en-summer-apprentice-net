using Microsoft.EntityFrameworkCore;
using TMS.Models;

namespace TMS.Repositories
{
    public class TicketCategoryRepository : ITicketCategoryRepository
    {
        private readonly TicketManagementDbContext _dbContext;
        public TicketCategoryRepository()
        {
            _dbContext = new TicketManagementDbContext();
        }
        public IEnumerable<TicketCategory> GetAll()
        {
            var ticketCategories = _dbContext.TicketCategories;

            return ticketCategories;
        }

        public async Task<TicketCategory> GetById(int? id)
        {
            var @ticketcategory = await _dbContext.TicketCategories.Where(t => t.TicketCategoryId == id).FirstOrDefaultAsync();

            return @ticketcategory;
        }

    }
}
