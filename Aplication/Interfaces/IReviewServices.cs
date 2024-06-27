using Aplication.Entities;

namespace Aplication.Interfaces
{
    public interface IReviewServices
    {
        IEnumerable<Review> GetReviews();
        Task<Review> GetReviewById(int id);
        Task CreateReview(Review review);
    }
}
