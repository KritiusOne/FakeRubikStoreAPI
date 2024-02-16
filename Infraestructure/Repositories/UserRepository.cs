using Aplication.Entities;
using Aplication.Interfaces;
using Infraestructure.Data;

namespace Infraestructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IRepository<User>
    {
        public UserRepository(FakeRubikStoreContext context) : base(context)
        {
            
        }
    }
}
