using Aplication.Interfaces;
using Aplication.Options;
using System.Security.Cryptography;

namespace Aplication.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly PasswordOptions _options;
        //private static readonly HashAlgorithmName _hashAlgorithmName = HashAlgorithmName.SHA256;
        //private const char _delimiter = ';';
        public PasswordService(PasswordOptions passwordOptions)
        {
            this._options = passwordOptions;
        }

        public bool Check(string hash, string password)
        {
            var parts = hash.Split('.');
            if (parts.Length != 3)
            {
                throw new FormatException("Unexpected hash format");
            }

            var iterations = Convert.ToInt32(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);

            using (var algorithm = new Rfc2898DeriveBytes(
                password,
                salt,
                iterations
                ))
            {
                var keyToCheck = algorithm.GetBytes(_options.KeySize);
                return keyToCheck.SequenceEqual(key);
            }
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
