namespace Aplication.Options
{
    public class PasswordOptions
    {
        public int SaltSize = 16;
        public int KeySize = 32;
        public int Iterations = 10000;
        public PasswordOptions()
        {
            
        }
    }
}
