namespace Aplication.Exceptions
{
    public class BaseException : Exception
    {
        public BaseException()
        {

        }
        public BaseException(string msg) : base(msg)
        { }
    }
}
