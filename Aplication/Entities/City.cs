namespace Aplication.Entities
{
    public class City : BaseEntity
    {
        public int IdDepartament { get; set; }
        public string Name { get; set; } = null!;
        public virtual Departament Departament { get; set; } = null!;
        public virtual ICollection<UserDirection> UserDirections { get; set; } = new List<UserDirection>();
    }
}
