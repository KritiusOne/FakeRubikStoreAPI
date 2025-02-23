namespace Aplication.DTOs.Orders
{
    public class CreateOrderWithFactusDTO
    {
        public int IdUser { get; set; }

        public DateTime Date { get; set; }

        public double FinalPrice { get; set; }
        public string NumberCard { get; set; } = null!;

        public ICollection<OrdersProductDTO> OrderProducts { get; set; } = new List<OrdersProductDTO>();
        public string CC { get; set; } = null!;
    }
}
