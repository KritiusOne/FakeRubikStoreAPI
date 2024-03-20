using Aplication.Entities;

namespace Aplication.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();
        Task AddProduct(Product product);

    }
}
