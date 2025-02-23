using Aplication.Entities;
using Aplication.Interfaces;

namespace Aplication.Services
{
    public class CardServices : ICardServices
    {
        private readonly IUnitOfWork<Card> _unitOfWork;
        public CardServices(IUnitOfWork<Card> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Card GetCardById(int id)
        {
            var card = _unitOfWork.CardRepo.FindCard(id);
            return card;
        }
        public async Task<int> CreateCard(Card card, int IdUser)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                if (card == null || IdUser <= 0)
                {
                    return -1;
                }

                var user = _unitOfWork.UserRepository.GetUserById(IdUser);
                if (user == null)
                {
                    throw new Exception("user not found");
                }
                if (user.IdCard != null)
                {
                    throw new Exception("You can't create a new card.");

                }
                var newCard = await _unitOfWork.CardRepo.CreateCard(card, user);
                await _unitOfWork.SaveChangesAsync();
                _unitOfWork.CommitTransaction();
                return newCard.Id;
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
                _unitOfWork.RollbackTransaction();
                return -1;
            }
            
        }


        public async Task<int> Updatecard(Card card, int IdUser)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                if (card == null) return -1;
                _unitOfWork.CardRepo.UpdateCard(card, IdUser);
                await _unitOfWork.SaveChangesAsync();
                _unitOfWork.CommitTransaction();
                return card.Id;
            }catch (Exception e)
            {
                Console.WriteLine(e.Message);
                _unitOfWork.RollbackTransaction();
                return -1;
            }

        }
    }
}
