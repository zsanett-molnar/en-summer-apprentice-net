using AutoMapper;
using TMS.Models;
using TMS.Models.Dto;
using TMS.Repositories;

namespace TMS.Services
{
    public class OrderService : IOrderService
    {

        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ITicketCategoryRepository _ticketCategoryRepository;


        public OrderService(IOrderRepository orderRepository, IMapper mapper, ITicketCategoryRepository ticketCategoryRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _ticketCategoryRepository = ticketCategoryRepository;
        }

        public async Task<Order> Add(OrderPostDto orderPost)
        {
            var @ticketCategory = await _ticketCategoryRepository.GetById(orderPost.TicketCategoryId);


            var newSum = orderPost.NumberOfTickets * @ticketCategory.Price;

            var newOrder = new Order()
            {
                OrderId = orderPost.OrderId,
                UserId = orderPost.UserId,
                TicketCategoryId = orderPost.TicketCategoryId,
                OrderedAt = DateTime.Now,
                NumberOfTickets = orderPost.NumberOfTickets,
                TotalPrice = newSum
            };

            _orderRepository.Add(newOrder);
            var result = _mapper.Map<Order>(newOrder);
            return result;
        }

        public async void Delete(int id)
        {
            var orderEntity = await _orderRepository.GetById(id);
            _orderRepository.Delete(orderEntity);
        }

        public IEnumerable<OrderDto> GetAll()
        {
            var orders = _orderRepository.GetAll();
            var ordersDto = _mapper.Map<List<OrderDto>>(orders);
            return ordersDto;
        }

        public async Task<OrderDto> GetById(int id)
        {
            var @order = await _orderRepository.GetById(id);
            var orderDto = _mapper.Map<OrderDto>(@order);
            return orderDto;
        }

        public async Task<OrderDto> Update(OrderPatchDto order)
        {
            var orderEntity = await _orderRepository.GetById(order.OrderId);

            if (order.NumberOfTickets != 0) orderEntity.NumberOfTickets = order.NumberOfTickets;

            var @ticketCategory = await _ticketCategoryRepository.GetById(orderEntity.TicketCategoryId);

            var newSum = orderEntity.NumberOfTickets * @ticketCategory.Price;
            orderEntity.TotalPrice = newSum;
            _orderRepository.Update(orderEntity);

            var orderDto = _mapper.Map<OrderDto>(orderEntity);
            return orderDto;

        }
    }
}
