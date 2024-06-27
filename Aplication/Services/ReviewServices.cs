using Aplication.Entities;
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
        public async Task<Review> GetReviewById(int id)
        {
            return await _unitOfWork.ReviewRepo.GetById(id);

        }
        public async Task CreateReview(Review review)
        {
            await _unitOfWork.ReviewRepo.Add(review);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
