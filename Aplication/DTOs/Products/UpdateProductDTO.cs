namespace Aplication.DTOs.Products
{
    public class UpdateProductDTO : ProductDTO
    {
        public ICollection<CategoryProductDTO> ProductCategories { get; set; } = new List<CategoryProductDTO>();
    }
}
