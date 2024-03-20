namespace API.Response
{
    public class ResponseBase<T>
    {
        public string Msg { get; set; }
        public T Response { get; set; }
        public ResponseBase(T res, string menssage) {
            this.Response = res;
            Msg = menssage;
        }
    }
}
