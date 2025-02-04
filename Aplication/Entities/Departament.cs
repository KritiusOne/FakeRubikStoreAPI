namespace Aplication.Entities
{
    public class Departament : BaseEntity
    {
        public int IdCountry { get; set; }
        public string Name { get; set; } = null!;
        public virtual ICollection<City> Cities { get; set; } = new List<City>();
        public virtual Country Country { get; set; } = null!;
    }
}
