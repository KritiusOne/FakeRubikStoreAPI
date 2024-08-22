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

        public bool ExistAllProducts(List<int> Ids)
        {
            return _context.Products
                .Where(prod => Ids.Contains(prod.Id))
                .Select(prod => prod.Id)
                .Distinct()
                .Count() == Ids.Count;

        }
    }
}
