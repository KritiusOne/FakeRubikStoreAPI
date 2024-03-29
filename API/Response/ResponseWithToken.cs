namespace API.Response
{
    public class ResponseWithToken<T> : ResponseBase<T>
    {
        public string Token { get; set; }
        public ResponseWithToken(string msg, T data, string token) : base(data, msg)
        {
            this.Token = token;
        }
    }
}
