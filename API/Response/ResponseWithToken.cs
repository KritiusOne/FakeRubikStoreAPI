namespace API.Response
{
    public class ResponseWithToken<T> : ResponseBase<T>
    {
        public string TypeToken { get; set; }
        public ResponseWithToken(string msg, T data, string TypeToken) : base(data, msg)
        {
            this.TypeToken = TypeToken;
        }
    }
}
