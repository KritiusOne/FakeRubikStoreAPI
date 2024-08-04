using Aplication.Entities;

namespace Aplication.Interfaces
{
    public interface IDirectionService
    {
        Task<UserDirection> CreateVoid();
        IEnumerable<UserDirection> GetAll();
        UserDirection GetById(int id);
        Task<UserDirection> Update(int id, UserDirection userDirection);
    }
}
