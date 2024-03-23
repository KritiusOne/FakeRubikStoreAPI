using Aplication.Interfaces;
using Aplication.QueryFilters;

namespace Aplication.Services
{
    public class UriService : IUriService
    {
        private readonly string _url;
        public UriService(string baseUri)
        {
            _url = baseUri;
        }
        public Uri GetPostPaginationUri(ProductQueryFilter Filters, string action)
        {
            string baseUrl = $"{action}";
            return new Uri(baseUrl);
        }
    }
}
