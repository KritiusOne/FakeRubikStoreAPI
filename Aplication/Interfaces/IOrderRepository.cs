using Aplication.Entities;

namespace Aplication.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        IEnumerable<Order> GetAllWithTables();
        Task<Order> AddAndReturn(Order order);
        Task AddNewOrderProduct(OrdersProducts orderProduct);
        Task<OrdersProducts> GetByIdOrderProdut(int ProductId, int OrderId);
        Task<Order> GetByIdWithTables(int id);
    }
}
