namespace Aplication.Interfaces
{
    public interface IFactusServices
    {
        Task<string> OAuth(string username, string password, string clientId, string ClientSecret);
        Task<string> Refresh(string username, string password, string clientId, string ClientSecret);
        Task<string> BillCreate(string token, string jsonBody, string typeToken);
        void SetURL(string URL);
        Task<string> BillValidate(string BillNumber, string token, string type_token);
    }
}
