using TMS.Models;

namespace TMS.Repositories
{
    public interface IVenueRepository
    {
        IEnumerable<Venue> GetAll();

        Task<Venue> GetById(int id);

        int Add(Venue @venue);

        void Update(Venue @venue);

        void Delete(Venue @venue);
    }
}
