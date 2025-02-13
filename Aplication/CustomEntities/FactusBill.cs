namespace Aplication.CustomEntities
{
    public class FactusBill
    {
        public int numbering_range_id { get; set; }
        public string reference_code { get; set; } = "I3";
        public string observation { get; set; } = "";
        public string payment_form { get; set; }
        public string payment_due_date { get; set; }
        public string payment_method_code { get; set; }
        // BillTime billing_period { get; set; } = null!;
        public ClientFactus customer { get; set; } = null!;
        public List<FactusItem> items { get; set; } = new List<FactusItem>();
        public FactusBill(string payment_method, List<FactusItem> items)
        {
            numbering_range_id = 8;
            reference_code = "I3";
            observation = "";
            payment_form = "1"; //Formas de pago, de contad;
            payment_method_code = payment_method;
            payment_due_date = "";
            //billing_period = new BillTime();
            this.items = items;
        }
    }
}
