namespace Aplication.CustomEntities
{
    public class ClientFactus
    {
        public int identification_document_id { get; set; }
        public string Identification { get; set; } = null!;
        public string? Dv { get; set; }
        public string? Company { get; set; }
        public string? Trade_name { get; set; }
        public string? Names { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int Legal_organization_id { get; set; } = 2;
        public int Tribute { get; set; }
        public int? Municipality_id { get; set; }
    }
}
