using Aplication.Entities;

namespace Aplication.Interfaces
{
    public interface IReviewServices
    {
        IEnumerable<Review> GetReviews();
        Review GetReviewById(int productId, int userId);
        Task CreateReview(Review review);
    }
}
