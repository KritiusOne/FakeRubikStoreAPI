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
        public async Task<User> AddAndReturnUser(User user)
        {
            await _entities.AddAsync(user);
            await _context.SaveChangesAsync();
            var newEntity = _entities.FirstOrDefault(e => e.Email == user.Email);
            return newEntity;
        }
        public User GetUserById(int id)
        {
            var user = _context.Users.Where(users => users.Id == id).FirstOrDefault();
            return user;
        }
    }
}
