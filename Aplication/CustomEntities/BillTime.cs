namespace Aplication.CustomEntities
{
    public class BillTime
    {
        public string start_date { get; set; } = null!;
        public string start_time { get; set; } = null!;
        public string end_date { get; set; } = null!;
        public string end_time { get; set; } = null!;
        public BillTime()
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            start_date = today.ToString("yyyy-MM-dd");
            end_date = today.ToString("yyyy-MM-dd");
            start_time = "";
            end_time = "";
        }
    }
}
