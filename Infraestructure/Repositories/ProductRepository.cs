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

        public IEnumerable<Product> GetAllWithTablesFilteredByCategories(List<int> categoriesIds)
        {
            var initialFilteredProducts = _context.Products
                .Include(p => p.ProductCategories)
                    .ThenInclude(pc => pc.CategoryNav)
                .Include(p => p.Reviews)
                .Where(p => p.ProductCategories.Any(pc => categoriesIds.Contains(pc.IdCategory)))
                .ToList();
            return initialFilteredProducts
                .Where(p => categoriesIds.All(Id => p.ProductCategories.Any(pc => pc.IdCategory == Id)));
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
