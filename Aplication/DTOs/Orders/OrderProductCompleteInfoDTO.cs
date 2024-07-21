using Aplication.DTOs.Products;
using Aplication.Entities;

namespace Aplication.DTOs.Orders
{
    public class OrderProductCompleteInfoDTO
    {
        public int IdProduct { get; set; }

        public int IdOrder { get; set; }

        public int ProductsNumber { get; set; }

        public double Price { get; set; }
        public ProductDTO ProductInfo { get; set; } = null!;
    }
}
