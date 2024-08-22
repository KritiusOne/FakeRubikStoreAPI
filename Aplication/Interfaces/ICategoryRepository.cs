using Aplication.Entities;

namespace Aplication.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        bool ExistAllProducts(List<int> Ids);
    }
}
