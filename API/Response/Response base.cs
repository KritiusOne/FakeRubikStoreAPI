namespace API.Response
{
    public class Response_base<T>
    {
        public string Msg { get; set; }
        public T Response { get; set; }
        public Response_base(T res, string menssage) {
            this.Response = res;
            Msg = menssage;
        }
    }
}
