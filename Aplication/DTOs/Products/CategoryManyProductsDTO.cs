namespace Aplication.DTOs.Products
{
    public class CategoryManyProductsDTO
    {
        public ICollection<CategoryProductDTO> categoryProductDTOs { get; set; } = new List<CategoryProductDTO>();
    }
}
