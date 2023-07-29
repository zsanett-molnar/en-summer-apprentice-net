using TMS.Models;
using TMS.Models.Dto;

namespace TMS.Services
{
    public interface IVenueService
    {
        IEnumerable<VenueDto> GetAll();

        Task<VenueDto> GetById(int id);

        int Add(VenueDto @venue);

        void Update(VenueDto @venue);

        void Delete(int id);
    }
}
