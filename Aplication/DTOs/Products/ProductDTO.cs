namespace Aplication.DTOs.Products
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public double Price { get; set; }

        public int Stock { get; set; }

        public string? Image { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Thumbnail { get; set; } = null!;
    }
}
