using Aplication.Entities;

namespace Aplication.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetAllWithTables();
        IEnumerable<Product> GetAllWithTablesFilteredByCategories(List<int> categoriesIds);
        Product GetByIdWithTables(int id);

    }
}
