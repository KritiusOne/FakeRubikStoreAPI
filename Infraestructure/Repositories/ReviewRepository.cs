using Aplication.Entities;
using Aplication.Interfaces;
using Infraestructure.Data;

namespace Infraestructure.Repositories
{
    public class ReviewRepository : BaseRepository<Review>, IReviewRepository
    {
        public ReviewRepository(FakeRubikStoreContext context) : base(context)
        {
            
        }

        public IEnumerable<Review> SearchReviewFromIdProduct(int idProduct)
        {
            var Reviews = _context.Reviews.Where(search => search.ProductId == idProduct).ToList();
            return Reviews;
        }
        public (Product, User) SearchUserAndProduct(int idProduct, int IdUser)
        {
            var FindProduct = _context.Products.FirstOrDefault(prod => prod.Id == idProduct);
            var FindUser = _context.Users.FirstOrDefault(user => user.Id == IdUser);
            return (FindProduct, FindUser);
        }
    }
}
