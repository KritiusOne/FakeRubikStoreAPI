namespace Aplication.CustomEntities
{
    public class FactusItem
    {
        public string code_reference { get; set; } = null!;
        public string name { get; set; } = null!;
        public int quantity { get; set; }
        public double discount_rate { get; set; }
        public double price { get; set; }
        public string tax_rate { get; set; } = "5.00";
        public int unit_measure_id { get; set; } = 70;
        public int standard_code_id { get; set; } = 1;
        public int is_excluded { get; set; }
        public int tribute_id { get; set; } = 1;
        public List<FactusRetention> withholding_taxes { get; set; } = new List<FactusRetention>();
        public FactusItem(string ItemName, int ItemQuantity, string ReferenceCode, double Pricing)
        {
            tax_rate = "5.00";
            unit_measure_id = 70;
            standard_code_id = 1;
            tribute_id = 1;
            discount_rate = 0;
            withholding_taxes = new List<FactusRetention>();

            name = ItemName;
            quantity = ItemQuantity;
            code_reference = ReferenceCode;
            price = Pricing;
        }
    }
}
