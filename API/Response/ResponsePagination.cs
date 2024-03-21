using Aplication.CustomEntities;

namespace API.Response
{
    public class ResponsePagination<T> : ResponseBase<T>
    {
        public MetaData MetaData { get; set; }
        public int StatusCode { get; set; }
        public ResponsePagination(T data, 
            string msg, 
            int StatusCode, 
            MetaData meta) : base(data, msg)
        {
            this.StatusCode = StatusCode;
            this.MetaData = meta;
        }
    }
}
