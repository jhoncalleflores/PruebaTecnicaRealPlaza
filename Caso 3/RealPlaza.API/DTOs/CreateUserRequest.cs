namespace RealPlaza.API.DTOs
{
    public class CreateUserRequest
    {
        public string Username { get; set; } = "";
        public string PasswordHash { get; set; } = "";
        public string Email { get; set; } = "";
        public DateTime BirthDate { get; set; }
    }
}
