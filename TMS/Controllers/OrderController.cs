using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using TMS.Models;
using TMS.Models.Dto;
using TMS.Repositories;
using TMS.Services;

namespace TMS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IOrderRepository _orderRepository;
        private readonly ITicketCategoryRepository _ticketCategoryRepository;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IOrderRepository orderRepository, ITicketCategoryRepository ticketCategoryRepository, IMapper mapper)
        {
            _orderService = orderService;
            _orderRepository = orderRepository;
            _ticketCategoryRepository = ticketCategoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<OrderDto>> GetAll()
        {
            var orders = _orderService.GetAll();
            if(orders == null)
            {
                return NotFound();
            }

            return Ok(_orderService.GetAll());

        }

        [HttpGet]
        public async Task<ActionResult<OrderDto>> GetById(int id)
        {

            var @order = await _orderService.GetById(id);
            if(@order == null)
            {
                return NotFound();
            }

            return Ok(@order);

        }

        [HttpPatch]
        public async Task<ActionResult<OrderDto>> Patch(OrderPatchDto orderPatch)
        {
         
            var order = await _orderService.Update(orderPatch);
            return Ok(order);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            _orderService.Delete(id);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> Add(OrderPostDto orderPost)
        {

            var addedOrder = await _orderService.Add(orderPost);
            if (orderPost == null)
            {
                return NotFound();
            }

            if (orderPost.NumberOfTickets == 0)
            {
                return NotFound();
            }

            if (orderPost.TicketCategoryId == null)
            {
                return NotFound();
            }

            return Ok(addedOrder);
        }


    }
}
