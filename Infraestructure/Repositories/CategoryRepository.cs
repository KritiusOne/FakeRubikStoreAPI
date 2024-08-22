using Aplication.Entities;
using Aplication.Interfaces;
using Infraestructure.Data;

namespace Infraestructure.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(FakeRubikStoreContext db) : base(db)
        {
            
        }

        public bool ExistAllCategories(List<int> Ids)
        {
            return _context.Categories
                .Where(ctg => Ids.Contains(ctg.Id))
                .Select(ctg => ctg.Id)
                .Distinct()
                .Count() == Ids.Count;
        }

        public bool ExistAllProducts(List<int> Ids)
        {
            return _context.Products
                .Where(prod => Ids.Contains(prod.Id))
                .Select(prod => prod.Id)
                .Distinct()
                .Count() == Ids.Count;
        }

        public async Task CreateProductCategory(ProductCategory entity)
        {
            await _context.AddAsync(entity);
        }
    }
}
