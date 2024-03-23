using Aplication.QueryFilters;

namespace Aplication.Interfaces
{
    public interface IUriService
    {
        Uri GetPostPaginationUri(ProductQueryFilter Filters,  string action);
    }
}
