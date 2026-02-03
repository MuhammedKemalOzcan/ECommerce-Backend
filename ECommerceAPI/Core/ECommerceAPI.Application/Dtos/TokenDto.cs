namespace ECommerceAPI.Application.Dtos
{
    public class TokenDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public bool IsAdmin { get; set; }
    }
}
