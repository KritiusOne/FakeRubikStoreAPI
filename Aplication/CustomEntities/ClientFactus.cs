namespace Aplication.CustomEntities
{
    public class ClientFactus
    {
        public string identification_document_id { get; set; }
        public string identification { get; set; } = null!;
        public string dv { get; set; } = null!;
        public string company { get; set; } = null!;
        public string trade_name { get; set; } = null!;
        public string names { get; set; } = null!;
        public string address { get; set; } = null!;
        public string email { get; set; } = null!;
        public string phone { get; set; } = null!;
        public string legal_organization_id { get; set; } = "2";
        public string tribute_id { get; set; }
        public string municipality_id { get; set; }
        public ClientFactus(string CC, string CustomerName, 
            string CustomerEmail, string CustomerPhone, 
            string Organization_Id,
            string CustomerPlace)
        {
            identification_document_id = "3"; // cedula de ciudadania
            address = "";
            company = "";
            dv = "";
            trade_name = "";
            tribute_id = "21";

            identification = CC;
            names = CustomerName;
            email = CustomerEmail;
            phone = CustomerPhone;
            legal_organization_id = Organization_Id; // persona natural
            municipality_id = "980";
        }
    }
}
