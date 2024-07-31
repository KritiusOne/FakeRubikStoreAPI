using Aplication.DTOs.Users;

namespace Aplication.DTOs
{
    public class AddressWithUserDTO : AddressDTO
    {
        public UserDTO? User { get; set; }
    }
}
