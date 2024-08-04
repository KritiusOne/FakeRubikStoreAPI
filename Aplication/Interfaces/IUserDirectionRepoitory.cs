using Aplication.Entities;

namespace Aplication.Interfaces
{
    public interface IUserDirectionRepoitory : IRepository<UserDirection>
    {
        UserDirection AddVoid(UserDirection userDirection);
        IEnumerable<UserDirection> GetAllWithUser();
        UserDirection GetByIdWithUserInfo(int id);

    }
}
