using Aplication.Entities;

namespace Aplication.DTOs.Users
{
    public class UserWithAddressDTO : UserDTO
    {
        public AddressDTO AdressInfo { get; set; } = null!;
    }
}
