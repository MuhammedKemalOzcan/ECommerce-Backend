namespace ECommerceAPI.Application.Dtos.UserDto
{
    public class AuthResultDto
    {
        public string UserId { get; set; }
        public bool Succeed { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public string? Error { get; set; }
        public bool IsAdmin { get; set; }
    }
}
