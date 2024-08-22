using Aplication.Entities;

namespace Aplication.Interfaces
{
    public interface ICategoryService
    {
        Task CreateCategory(Category newCategory);
    }
}
