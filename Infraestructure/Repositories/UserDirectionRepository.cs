using Aplication.Entities;
using Aplication.Interfaces;
using Infraestructure.Data;

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
        
    }
}
