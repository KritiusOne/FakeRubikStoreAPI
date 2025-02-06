using Aplication.Entities;

namespace Aplication.DTOs.Cards
{
    public class CardDTO
    {
        public int Id { get; set; }
        public int IdCardType { get; set; }
        public string CardNumber { get; set; } = null!;
        public CardTypeDTO Type { get; set; } = null!;
    }
}
