namespace Aplication.CustomEntities
{
    public class FactusItem
    {
        public string Code_reference { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int Quantity { get; set; }
        public double Discount_rate { get; set; }
        public decimal Price { get; set; }
        public string Tax_rate { get; set; } = "5.00";
        public int Unit_measure_id { get; set; } = 70;
        public int Standard_code_id { get; set; } = 1;
        public bool Is_excluded { get; set; } = true;
        public int Tribute_id { get; set; } = 1;
        public List<FactusRetention> Withholding_taxes { get; set; } = new List<FactusRetention>();
    }
}
