using Aplication.CustomEntities;
using Aplication.Entities;
using Aplication.QueryFilters;

namespace Aplication.Interfaces
{
    public interface IProductService
    {
        PagedList<Product> GetAllProducts(ProductQueryFilter filters);
        Task AddProduct(Product product);
        Task<Product> GetById(int id);
    }
}
