namespace Aplication.DTOs.Users
{
    public class UserMinimalDTO
    {
        public int Id { get; set; }
        public int IdRole { get; set; }

        public int IdAddress { get; set; }

        public string? Name { get; set; }

        public string? SecondName { get; set; }

        public string Email { get; set; } = null!;
    }
}
