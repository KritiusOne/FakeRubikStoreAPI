using Aplication.Entities;
using Aplication.Interfaces;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(FakeRubikStoreContext context) : base(context)
        {
            
        }

        public IEnumerable<Product> GetAllWithTables()
        {
            return _context.Products
                .Include(p => p.OrderProducts)
                .Include(p => p.ProductCategories)
                .Include(p => p.Reviews)
                .ToList();
        }
    }
}
