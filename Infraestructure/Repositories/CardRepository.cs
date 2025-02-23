using Aplication.Entities;
using Aplication.Interfaces;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class CardRepository : BaseRepository<Card>, ICardRepository
    {
        public CardRepository(FakeRubikStoreContext context)  : base(context) { }

        public async Task<Card?> CreateCard(Card card, User userInfo)
        {
            await _context.Cards.AddAsync(card);

            await _context.SaveChangesAsync();
            userInfo.IdCard = card.Id;

            _context.Users.Update(userInfo);

            return card;
        }

        public Card FindCard(int Id)
        {
            var card = _context.Cards
                .Include(x => x.Type)
                .FirstOrDefault(e => e.Id == Id);
            return card;
        }

        public Card? UpdateCard(Card card, int UserId)
        {
            var userInfo = _context.Users
                .Include(e => e.InfoCard)
                .FirstOrDefault(x => x.Id == UserId);
            if (userInfo == null || card == null) return null;

            userInfo.InfoCard.CardNumber = card.CardNumber;
            userInfo.InfoCard.IdCardType = card.IdCardType;
            userInfo.IdCard = card.Id;

            _context.Users.Update(userInfo);

            return card;
        }
    }

}
