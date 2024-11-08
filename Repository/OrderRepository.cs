using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Interfaces;
using Repository.Model;

namespace Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OMSdbContext _context;

        public OrderRepository(OMSdbContext context)
        {
            _context = context;
        }
        public bool Add(Order order)
        {
            _context.Orders.Add(order);
            int status = _context.SaveChanges();
            if (status >= 0)
                return true;
            return false;
        }

        public bool Delete(int id)
        {
            var order = _context.Orders.Find(id);
            if (order != null)
            {
                var details = _context.orderDetails.Where(x => x.OrderId == id).ToList();
                if (details.Count>0)
                {
                    _context.orderDetails.RemoveRange(details);
                }
                _context.Orders.Remove(order);
                int status = _context.SaveChanges();
                if (status >= 0)
                    return true;
                return false;
            }
            else
           return true;
        }

        public IEnumerable<Order> GetAll()
        {
           return _context.Orders.Include(x=>x.OrderDetails).AsNoTracking().ToList();
        }

        public Order GetById(int id)
        {
            var order = _context.Orders
        .Include(d => d.OrderDetails)
        .FirstOrDefault(x => x.Id == id);

            if (order != null && order.OrderDetails == null)
            {
                order.OrderDetails = new List<OrderDetails>();
            }

            return order;
        }

        public bool Update(Order order)
        {
            _context.Entry(order).State = EntityState.Modified;
            _context.Entry(order).Property(x => x.EntryDate).IsModified = false;
            foreach (var orderDetail in order.OrderDetails)
            {
                _context.Entry(orderDetail).State = EntityState.Modified;
            }

            _context.SaveChanges();
            return true;
        }
    }
}
