namespace Bookie.Application.DTOs
{
    public class AuthResponseDto
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string RoleName { get; set; }
        public string Token { get; set; }
    }
}
