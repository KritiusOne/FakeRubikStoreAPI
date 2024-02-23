using Aplication.Entities;

namespace Aplication.Interfaces
{
    public interface IDirectionService
    {
        Task<UserDirection> CreateVoid();
        IEnumerable<UserDirection> GetAll();
    }
}
