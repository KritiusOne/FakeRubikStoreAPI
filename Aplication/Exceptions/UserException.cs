namespace Aplication.Exceptions
{
    public class UserException : Exception
    {
        public UserException()
        {
            
        }
        public UserException(string msg) : base(msg) 
        {  }

    }
}
