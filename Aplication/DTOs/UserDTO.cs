namespace Aplication.DTOs
{
    public class UserDTO
    {
        public int IdRole { get; set; }

        public int IdAddress { get; set; }

        public string? Name { get; set; }

        public string? SecondName { get; set; }

        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;
    }
}
