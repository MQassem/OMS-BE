using Repository.Interfaces;
using Repository.Model;
using Service.DTO;
using Service.Intercaces;

namespace Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repo;
        public OrderService(IOrderRepository rebo)
        {
            _repo = rebo;
        }
        public bool Add(OrderDTO orderDTO)
        {
            Order order = MappingToOrder(orderDTO);
            return _repo.Add(order);
        }
        public bool Delete(int id) => _repo.Delete(id);
        public IEnumerable<OrderDTO> GetAll() => MappingToDTO(_repo.GetAll().ToList());
        public OrderDTO GetById(int id) => MappingToDTO(_repo.GetById(id));
        public bool Update(OrderDTO order) => _repo.Update(MappingToOrder(order));

        private Order MappingToOrder(OrderDTO dto)
        {
            var order = new Order
            {
                Id = dto.Id,
                ClientId = dto.ClientId,
                EntryUserId = dto.UserId,
                Price = dto.TotalPrice,

            };
            order.OrderDetails = new List<OrderDetails>();
            foreach (var item in dto.Details)
                order.OrderDetails.Add(new OrderDetails
                {
                    Id = item.Id,
                    ItemId = item.itemCode,
                    ItemPrice = item.Price,
                });
            return order;
        }
        private OrderDTO MappingToDTO(Order order)
        {
            var orderDto = new OrderDTO()
            {
                Id = order.Id,
                ClientId = order.ClientId,
                TotalPrice = order.Price,
                UserId = order.EntryUserId,
            };
            orderDto.Details = new List<OrderDetailsDTO>();
            foreach (var item in order.OrderDetails)
                orderDto.Details.Add(new OrderDetailsDTO
                {
                    Id = item.Id,
                    itemCode = item.ItemId,
                    Price = item.ItemPrice,
                });
            return orderDto;
        }
        private List<Order> MappingToOrder(List<OrderDTO> dtos)
        {
            List<Order> orders = new List<Order>();
            foreach (OrderDTO dto in dtos)
            {
                var order = new Order
                {
                    Id = dto.Id,
                    ClientId = dto.ClientId,
                    EntryUserId = dto.UserId,
                    Price = dto.TotalPrice,

                };
                order.OrderDetails = new List<OrderDetails>();
                foreach (var item in dto.Details)
                    order.OrderDetails.Add(new OrderDetails
                    {
                        Id = item.Id,
                        ItemId = item.itemCode,
                        ItemPrice = item.Price,
                    });
                orders.Add(order);
            }
            return orders;
        }
        private List<OrderDTO> MappingToDTO(List<Order> orders)
        {
            List<OrderDTO> dtos = new List<OrderDTO>();
            foreach (Order order in orders)
            {
                var orderDto = new OrderDTO()
                {
                    Id = order.Id,
                    ClientId = order.ClientId,
                    TotalPrice = order.Price,
                    UserId = order.EntryUserId,
                };
                orderDto.Details = new List<OrderDetailsDTO>();
                foreach (var item in order.OrderDetails)
                    orderDto.Details.Add(new OrderDetailsDTO
                    {
                        Id = item.Id,
                        itemCode = item.ItemId,
                        Price = item.ItemPrice,
                    });
                dtos.Add(orderDto);
            }
            return dtos;
        }
    }
}
