namespace Aplication.DTOs.Orders
{
    public class OrderBasicDTO
    {
        public int Id { get; set; }
        public int IdUser { get; set; }

        public int IdDelivery { get; set; }

        public DateTime Date { get; set; }

        public double FinalPrice { get; set; }
    }
}
