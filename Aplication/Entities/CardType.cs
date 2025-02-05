namespace Aplication.Entities
{
    public class CardType : BaseEntity
    {
        public int Code { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Card> Cards { get; set; } = new List<Card>();
    }
}
