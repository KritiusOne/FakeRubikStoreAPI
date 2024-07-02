using Aplication.Entities;
using Aplication.Exceptions;
using Aplication.Interfaces;

namespace Aplication.Services
{
    public class ReviewServices : IReviewServices
    {
        private readonly IUnitOfWork<Review> _unitOfWork;
        public ReviewServices(IUnitOfWork<Review> unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public IEnumerable<Review> GetReviews()
        {
            return _unitOfWork.ReviewRepo.GetAll();
        }
        public Review GetReviewById(int productId, int userId)
        {
            return _unitOfWork.ReviewRepo.GetReview(productId, userId);

        }
        public async Task CreateReview(Review review)
        {
            var members = _unitOfWork.ReviewRepo.SearchUserAndProduct(review.ProductId, review.UserId);
            if (members.Item2 == null)
            {
                throw new BaseException("El usuario que intentó hacer la review, no existe");
            }
            if(members.Item1 == null)
            {
                throw new BaseException("El producto al que se le intento realizar la review no existe");
            }
            review.Product = members.Item1;
            review.Usuario = members.Item2;
            await _unitOfWork.ReviewRepo.Add(review);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateReview(int productId, int userId, Review review)
        {
            var reviewActual = _unitOfWork.ReviewRepo.GetReview(productId, userId);
            if(reviewActual == null)
            {
                throw new BaseException("The review to modify not exist");
            }
            reviewActual.Description = review.Description;
            reviewActual.Rate = review.Rate;
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
