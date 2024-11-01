namespace Aplication.QueryFilters
{
    public class ProductQueryFilter
    {
        public int? ProductID { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public string? NameProduct { get; set; }
        public string? DescriptionProduct { get; set; }
        public int PageSize { get; set; } = 20;
        public int PageNumber { get; set; } = 1;
    }
}
