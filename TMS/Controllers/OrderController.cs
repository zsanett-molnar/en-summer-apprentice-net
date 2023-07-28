using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using TMS.Models;
using TMS.Models.Dto;
using TMS.Repositories;

namespace TMS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ITicketCategoryRepository _ticketCategoryRepository;
        private readonly IMapper _mapper;

        public OrderController(IOrderRepository orderRepository, ITicketCategoryRepository ticketCategoryRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _ticketCategoryRepository = ticketCategoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<OrderDto>> GetAll()
        {

            var orders = _orderRepository.GetAll();

            var dtoOrders = orders.Select(o => new OrderDto()
            {
                OrderId = o.OrderId,
                OrderedAt = o.OrderedAt,
                NumberOfTickets = o.NumberOfTickets,
                TotalPrice = o.TotalPrice
            });

            return Ok(dtoOrders);

        }

        [HttpGet]
        public async Task<ActionResult<OrderDto>> GetById(int id)
        {

            var @order = await _orderRepository.GetById(id);

            if (@order == null)
            {
                return NotFound();
            }


            var orderDto = _mapper.Map<OrderDto>(@order);

            return Ok(orderDto);

        }

        [HttpPatch]
        public async Task<ActionResult<OrderDto>> Patch(OrderPatchDto orderPatch)
        {
            var orderEntity = await _orderRepository.GetById(orderPatch.OrderId);

            if (orderEntity == null)
            {
                return NotFound();
            }

            if (orderPatch.NumberOfTickets != 0) orderEntity.NumberOfTickets = orderPatch.NumberOfTickets;

            var @ticketCategory = await _ticketCategoryRepository.GetById(orderEntity.TicketCategoryId);

            if (@ticketCategory == null)
            {
                return NotFound();
            }

            var newSum = orderEntity.NumberOfTickets * @ticketCategory.Price;
            orderEntity.TotalPrice = newSum;
            _orderRepository.Update(orderEntity);
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var orderEntity = await _orderRepository.GetById(id);
            if (orderEntity == null)
            {
                return NotFound();
            }
            _orderRepository.Delete(orderEntity);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> Add(OrderPostDto orderPost)
        {
            if (orderPost == null)
            {
                return NotFound();
            }

            if (orderPost.NumberOfTickets == 0)
            {
                return NotFound();
            }

            var @ticketCategory = await _ticketCategoryRepository.GetById(orderPost.TicketCategoryId);

            if (@ticketCategory == null)
            {
                return NotFound();
            }

            var newSum = orderPost.NumberOfTickets * @ticketCategory.Price;

            var newOrder = new Order()
            {
                 OrderId= orderPost.OrderId,
                 UserId = orderPost.UserId,
                 TicketCategoryId = orderPost.TicketCategoryId,
                 OrderedAt = DateTime.Now,
                 NumberOfTickets = orderPost.NumberOfTickets,
                 TotalPrice= newSum
            };

            _orderRepository.Add(newOrder);
            return Ok(newOrder.OrderId);
        }


    }
}
