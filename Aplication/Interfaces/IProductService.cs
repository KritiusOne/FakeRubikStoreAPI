using Aplication.CustomEntities;
using Aplication.Entities;
using Aplication.QueryFilters;

namespace Aplication.Interfaces
{
    public interface IProductService
    {
        PagedList<Product> GetAllProducts(ProductQueryFilter filters);
        Task AddProduct(Product product, Stream ThumbnailImg, Stream ProductImg);
        Product GetById(int id);
        Task UpdateProduct(Stream thumbnailImg, Stream productImg, Product ProductInfo, int id);
    }
}
