using Aplication.Entities;

namespace Aplication.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        bool ExistAllProducts(List<int> Ids);
        bool ExistAllCategories(List<int> Ids);
        Task CreateProductCategory(ProductCategory entity);
    }
}
