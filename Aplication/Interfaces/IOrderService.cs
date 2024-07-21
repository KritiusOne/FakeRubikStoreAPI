using Aplication.CustomEntities;
using Aplication.Entities;
using Aplication.QueryFilters;

namespace Aplication.Interfaces
{
    public interface IOrderService
    {
        PagedList<Order> GetAll(OrderQueryFilters filters);
        Task<Order> CreateOrder(Order order);
        Task<Order> GetById(int id);
    }
}
