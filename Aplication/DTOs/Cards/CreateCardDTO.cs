namespace Aplication.DTOs.Cards
{
    public class CreateCardDTO
    {
        public int IdCardType { get; set; }
        public string CardNumber { get; set; } = null!;
    }
}
