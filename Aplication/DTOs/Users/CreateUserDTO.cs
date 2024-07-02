namespace Aplication.DTOs.Users
{
    public class CreateUserDTO
    {
        public string? Name { get; set; }
        public string? SecondName { get; set; }
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
