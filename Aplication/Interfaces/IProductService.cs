using Aplication.CustomEntities;
using Aplication.Entities;

namespace Aplication.Interfaces
{
    public interface IProductService
    {
        PagedList<Product> GetAllProducts();
        Task AddProduct(Product product);

    }
}
