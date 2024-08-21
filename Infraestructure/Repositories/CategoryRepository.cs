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
    }
}
