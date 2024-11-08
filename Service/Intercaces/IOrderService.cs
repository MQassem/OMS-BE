using Repository.Model;
using Service.DTO;

namespace Service.Intercaces
{
    public interface IOrderService
    {
        IEnumerable<OrderDTO> GetAll();
        OrderDTO GetById(int id);
        bool Add(OrderDTO order);
        bool Update(OrderDTO order);
        bool Delete(int id);
    }
}
