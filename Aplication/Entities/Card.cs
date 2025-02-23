namespace Aplication.Entities
{
    public class Card : BaseEntity
    {
        public int IdCardType { get; set; }
        public string CardNumber { get; set; } = null!;
        public virtual CardType Type { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
