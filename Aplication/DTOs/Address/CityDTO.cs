namespace Aplication.DTOs.Address
{
    public class CityDTO
    {
        public int IdDepartament { get; set; }
        public string NameCity { get; set; } = null!;
        public DepartamentDTO departament { get; set; }
    }
}
