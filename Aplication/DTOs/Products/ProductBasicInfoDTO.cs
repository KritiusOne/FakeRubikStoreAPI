namespace Aplication.DTOs.Products
{
    public class ProductBasicInfoDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public double Price { get; set; }

        public string Thumbnail { get; set; } = null!;
    }
}
