namespace Aplication.QueryFilters
{
    public class OrderQueryFilters
    {
        public int PageSize { get; set; } = 20;
        public int PageNumber { get; set; } = 1;
        public DateTime? Date { get; set; }
        public DateTime? MinDate { get; set; }
        public DateTime? MaxDate { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public int? IdUser { get; set; }
    }
}
