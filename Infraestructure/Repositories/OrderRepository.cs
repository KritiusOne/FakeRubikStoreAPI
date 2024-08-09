using Aplication.Entities;
using Aplication.Interfaces;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(FakeRubikStoreContext db) : base(db)
        {}

        public async Task<Order> AddAndReturn(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task AddNewOrderProduct(OrdersProducts orderProduct)
        {
            await _context.ProductsOrders.AddAsync(orderProduct);
        }

        public IEnumerable<Order> GetAllWithTables()
        {
            return _context.Orders
                .Include(e => e.UserInfo)
                .Include(e => e.DeliveryInfo)
                .Include(e => e.OrderProducts)
                    .ThenInclude(x => x.ProductInfo)
                .ToList();
        }

        public async Task<OrdersProducts> GetByIdOrderProdut(int ProductId, int OrderId)
        {
            return await _context.ProductsOrders.FirstAsync(x => x.IdProduct == ProductId && x.IdOrder == OrderId);
        }

        public async Task<Order> GetByIdWithTables(int id)
        {
            return await _context.Orders
                .Include(h => h.UserInfo)
                    .ThenInclude(h => h.AdressInfo)
                .Include(h => h.OrderProducts)
                    .ThenInclude(x => x.ProductInfo)
                .Include(h => h.DeliveryInfo)
                .FirstAsync(h => h.Id == id);
        }
    }
}
