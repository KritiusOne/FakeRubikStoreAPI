using Aplication.Entities;

namespace Aplication.Interfaces
{
    public interface ICategoryService
    {
        Task CreateCategory(Category newCategory);
        Task CreateManyProductsCategories(ICollection<ProductCategory> productsCategories);
    }
}
