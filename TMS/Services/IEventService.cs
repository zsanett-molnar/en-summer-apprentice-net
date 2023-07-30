using TMS.Models;
using TMS.Models.Dto;

namespace TMS.Services
{
    public interface IEventService
    {
        IEnumerable<EventDto> GetAll();

        Task<EventDto> GetById(int id);

        Event Add(EventPostDto @event);

        void Update(EventPatchDto @event);

        void Delete(int id);
    }
}
