namespace Aplication.Entities
{
    public class Country : BaseEntity
    {
        public string Name { get; set; } = null!;
        public virtual ICollection<Departament> Departaments { get; set; } = new List<Departament>();
    }
}
