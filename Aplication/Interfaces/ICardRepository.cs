using Aplication.Entities;

namespace Aplication.Interfaces
{
    public interface ICardRepository : IRepository<Card>
    {
        Task<Card?> CreateCard(Card card, User userInfo);
        Card FindCard(int Id);
        Card? UpdateCard(Card card, int UserId);
    }
}
