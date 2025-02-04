namespace API.CustomClass.External
{
    public class FactusResponseBase<T>
    {
        public string Status { get; set; } = null!;
        public string Message { get; set; } = null!;
        public List<T> Data { get; set; } = null!;
    }
}
