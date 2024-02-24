using Aplication.Entities;
using Aplication.Interfaces;
using Infraestructure.Data;

namespace Infraestructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(FakeRubikStoreContext context) : base(context)
        {
            
        }
        public User GetUserByCredentials(string email)
        {
            var user = _context.Users.Where(p => p.Email == email).FirstOrDefault();
            return user;
        }
    }
}
