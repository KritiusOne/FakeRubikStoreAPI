namespace Aplication.DTOs.Address
{
    public class DepartamentDTO
    {
        public int IdCountry { get; set; }
        public string NameDepartament { get; set; } = null!;
        public CountryDTO Country { get; set; } = null!;
    }
}
