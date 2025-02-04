using Aplication.Entities;

namespace Aplication.Interfaces
{
    public interface IUserDirectionRepoitory : IRepository<UserDirection>
    {
        UserDirection AddVoid(UserDirection userDirection);
        IEnumerable<UserDirection> GetAllWithUser();
        UserDirection GetByIdWithUserInfo(int id);
        Task<List<Departament>> CreateDepartments(List<Departament> departments);
        Task<List<City>> CreateCities(List<City> Cities);
    }
}
