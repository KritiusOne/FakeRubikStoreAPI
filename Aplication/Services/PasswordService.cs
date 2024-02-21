using Aplication.Interfaces;
using Aplication.Options;
using System.Security.Cryptography;

namespace Aplication.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly PasswordOptions _options;
        public PasswordService(PasswordOptions passwordOptions)
        {
            this._options = passwordOptions;
        }

        public bool Check(string hash, string password)
        {
            throw new NotImplementedException();
        }

        public string Hash(string password)
        {
            using (var algorithm = new Rfc2898DeriveBytes(
               password,
               _options.SaltSize,
               _options.Iterations
               ))
            {
                var key = Convert.ToBase64String(algorithm.GetBytes(_options.KeySize));

                var salt = Convert.ToBase64String(algorithm.Salt);
                return $"{_options.Iterations}.{salt}.{key}";
            }
        }
    }
}
