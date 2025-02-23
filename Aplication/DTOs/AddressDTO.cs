using Aplication.DTOs.Address;

namespace Aplication.DTOs
{
    public class AddressDTO
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public CityDTO UserCity { get; set; } = null!;
    }
}
