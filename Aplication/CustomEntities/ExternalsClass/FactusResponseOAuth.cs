namespace Aplication.CustomEntities.ExternalsClass
{
    public class FactusResponseOAuth
    {
        public string token_type { get; set; } = null!;
        public int expires_in { get; set; }
        public string access_token { get; set; } = null!;
        public string refresh_token { get; set; } = null!;
    }
}
