namespace ECommerceAPI.Application.Features.Commands.LoginUser
{
    public class LoginUserCommandResponse
    {
        public bool Succeed { get; set; }
        public string? AccesToken { get; set; }
        public string? RefreshToken { get; set; }
        public string? Error { get; set; }
    }
}
