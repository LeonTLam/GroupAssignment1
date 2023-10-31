using GroupAssignment1.Models;

namespace GroupAssignment1.DAL
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>?> GetAll();
        Task<Housing?> GetHousingById(int id);
        Task<Order?> GetOrderById(int id);
        Task<bool> CreateOrder(Order order);
        Task<bool> Delete(int id);
    }
}
