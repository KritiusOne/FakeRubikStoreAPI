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


    }
}
