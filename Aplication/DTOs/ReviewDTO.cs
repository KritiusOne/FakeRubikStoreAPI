namespace Aplication.DTOs
{
    public class ReviewDTO
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int UserId { get; set; }

        public string? Description { get; set; }

        public int Rate { get; set; }
    }
}
