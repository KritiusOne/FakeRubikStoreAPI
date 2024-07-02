using Aplication.Entities;

namespace Aplication.DTOs
{
    public class ProductCategoryDTO
    {
        public int IdCategory { get; set; }

        public int IdProduct { get; set; }

        public CategoryDTO CategoryNav { get; set; } = null!;
    }
}
