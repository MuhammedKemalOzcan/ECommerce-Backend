using ECommerceAPI.Application.Dtos;

namespace ECommerceAPI.Application.Abstractions
{
    public interface ITokenHandler
    {
        //Token içerisindeki userId ve Email ile istekleri atan kişiyi claim üzerinden bulmak kolaylaşır, Role ile ise bu isteği atan kişinin rolünü ekstra sorgulamamıza gerek kalmaz.
        TokenDto GenerateToken(string userId, string email, IEnumerable<string> roles);
        string GenerateRefreshToken();
    }
}
