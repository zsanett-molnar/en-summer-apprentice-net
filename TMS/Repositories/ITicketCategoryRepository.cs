using TMS.Models;

namespace TMS.Repositories
{
    public interface ITicketCategoryRepository
    {

        IEnumerable<TicketCategory> GetAll();

        Task<TicketCategory> GetById(int? id);
    }
}
