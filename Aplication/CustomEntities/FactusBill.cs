namespace Aplication.CustomEntities
{
    public class FactusBill
    {
        public int Numbering_range_id { get; set; }
        public string Reference_code { get; set; } = "I3";
        public string Observation { get; set; } = "";
        public int Payment_form { get; set; }
        public DateTime Payment_due_date { get; set; }
        public int Payment_method_code { get; set; }
        public BillTime? Billing_period { get; set; }
        public ClientFactus Customer { get; set; } = null!;
        public List<FactusItem> Items { get; set; } = new List<FactusItem>();
    }
}
