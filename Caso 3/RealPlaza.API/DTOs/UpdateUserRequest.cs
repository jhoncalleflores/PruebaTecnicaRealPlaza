namespace RealPlaza.API.DTOs
{
    public class UpdateUserRequest
    {
        public string Username { get; set; } = "";
        public string Email { get; set; } = "";
        public DateOnly BirthDate { get; set; }
        public bool IsActive { get; set; }
    }
}
