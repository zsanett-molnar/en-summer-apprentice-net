using TMS.Models;
using TMS.Models.Dto;

namespace TMS.Services
{
    public interface IVenueService
    {
        IEnumerable<Venue> GetAll();

        Task<VenueDto> GetById(int id);

        int Add(VenueDto @venue);

        void Update(Venue @venue);

        void Delete(Venue @venue);
    }
}
