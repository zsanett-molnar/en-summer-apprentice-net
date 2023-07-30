using TMS.Models;
using TMS.Models.Dto;

namespace TMS.Services
{
    public interface IOrderService
    {
        IEnumerable<OrderDto> GetAll();

        Task<OrderDto> GetById(int id);

        Task<Order> Add(OrderPostDto @order);

        Task<OrderDto> Update(OrderPatchDto @order);

        void Delete(int id);
    }
}
