using Aplication.DTOs.Products;

namespace API.CustomClass
{
    public class CreateProductWithImgDTO
    {
        public CreateProductDTO InfoProduct { get; set; } = null!;
        public IFormFile ThumbnailImage { get; set; } = null!;
        public IFormFile ProductImage { get; set; } = null!;

    }
}
