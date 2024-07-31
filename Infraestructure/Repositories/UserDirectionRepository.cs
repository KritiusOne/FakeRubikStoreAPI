using Aplication.Entities;
using Aplication.Interfaces;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class UserDirectionRepository : BaseRepository<UserDirection>, IUserDirectionRepoitory
    {
        public UserDirectionRepository(FakeRubikStoreContext context) : base(context) { }
        public UserDirection AddVoid(UserDirection newAddress)
        {
            base._entities.Add(newAddress);
            return newAddress;
        }
        public IEnumerable<UserDirection> GetAllWithUser()
        {
            return _context.Directions
                .Include(e => e.User)
                .ToList();
        }

        public UserDirection GetByIdWithUserInfo(int id)
        {
            return _context.Directions
                .Where(e => e.Id == id)
                .Include(e => e.User)
                .FirstOrDefault();
        }
    }
}
