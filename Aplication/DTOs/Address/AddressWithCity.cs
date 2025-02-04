namespace Aplication.DTOs.Address
{
    public class AddressWithCity : AddressDTO
    {
        public CityDTO City { get; set; } = null!;
    }
}
