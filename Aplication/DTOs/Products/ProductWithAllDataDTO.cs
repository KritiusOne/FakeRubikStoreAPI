using Aplication.Entities;

namespace Aplication.DTOs.Products
{
    public class ProductWithAllDataDTO : ProductDTO
    {
        public ICollection<ReviewDTO> Reviews { get; set; } = new List<ReviewDTO>();

        public ICollection<ProductCategoryDTO> ProductCategories { get; } = new List<ProductCategoryDTO>();
    }
}
