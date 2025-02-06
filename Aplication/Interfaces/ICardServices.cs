using Aplication.Entities;

namespace Aplication.Interfaces
{
    public interface ICardServices
    {
        Card GetCardById(int id);
        Task<int> CreateCard(Card card, int IdUser);
        Task<int> Updatecard(Card card, int IdUser);
    }
}
