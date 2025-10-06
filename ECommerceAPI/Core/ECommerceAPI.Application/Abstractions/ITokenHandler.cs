using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Abstractions
{
    public interface ITokenHandler
    {
        //Token içerisindeki userId ve Email ile istekleri atan kişiyi claim üzerinden bulmak kolaylaşır, Role ile ise bu isteği atan kişinin rolünü ekstra sorgulamamıza gerek kalmaz.
        string GenerateToken(string userId, string email, IEnumerable<string> roles);
    }
}
