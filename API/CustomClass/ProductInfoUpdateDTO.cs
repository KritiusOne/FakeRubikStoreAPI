using Aplication.DTOs.Products;

namespace API.CustomClass
{
    //Nota: Lo creo aquí por el tipo IFormFile
    public class ProductInfoUpdateDTO 
    {
        public ProductDTO InfoProduct { get; set; } = null!;
        public IFormFile ThumbnailImage { get; set; } = null!;
        public IFormFile ProductImage { get; set; } = null!;

    }
}
