using Aplication.CustomEntities;
using Aplication.Entities;
using Aplication.QueryFilters;

namespace Aplication.Interfaces
{
    public interface IProductService
    {
        PagedList<Product> GetAllProducts(ProductQueryFilter filters);
        Task AddProduct(Product product);
        Product GetById(int id);
        Task UpdateProduct(Stream thumbnailImg, Stream productImg, Product ProductInfo, string blobKey, int id);
    }
}
