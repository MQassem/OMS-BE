using Repository.Model;

namespace Repository.Interfaces
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAll();
        Order GetById(int id);
        bool Add(Order order);
        bool Update(Order order);
        bool Delete(int id);
    }
}
