namespace Aplication.DTOs.Products
{
    public class CreateCategoryDTO
    {
        public string Name { get; set; } = null!;

        public ICollection<CategoryProductDTO> ProductCategories { get; set; } = new List<CategoryProductDTO>();
    }
}
