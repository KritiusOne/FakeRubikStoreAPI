using Aplication.Entities;

namespace Aplication.Interfaces
{
    public interface IReviewRepository : IRepository<Review>
    {
        IEnumerable<Review> SearchReviewFromIdProduct(int idProduct);
        (Product, User) SearchUserAndProduct(int idProduct, int IdUser);
    }
}
