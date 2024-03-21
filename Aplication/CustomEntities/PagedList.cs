namespace Aplication.CustomEntities
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public bool hasPreviousPage => CurrentPage > 1;
        public bool hasNextPage => CurrentPage < TotalPages;
        public int? NextPageNumber => hasNextPage ? CurrentPage + 1 : (int?)null;
        public int? PreviousPageNumber => hasPreviousPage ? CurrentPage - 1 : (int?)null;
        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            this.CurrentPage = pageNumber;
            this.PageSize = pageSize;
            this.PageCount = count;
            this.TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }
        public static PagedList<T> CreatedPagedList(IEnumerable<T> values, int pageNumber, int pageSize)
        {
            var count = values.Count();
            var items = values.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            var returnItems = items.ToList<T>();
            return new PagedList<T>(returnItems, count, pageNumber, pageSize);
        }
    }
}
