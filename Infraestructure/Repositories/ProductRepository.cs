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
                .Include(p => p.ProductCategories)
                    .ThenInclude(p => p.CategoryNav)
                .Include(p => p.Reviews)
                .ToList();
        }

        public Product GetByIdWithTables(int id)
        {
            var product = _context.Products
                .Include(p => p.ProductCategories)
                    .ThenInclude(ctg => ctg.CategoryNav)
                .Include(p => p.Reviews)
                .FirstOrDefault(prod => prod.Id == id);
            return product;
        }
    }
}
