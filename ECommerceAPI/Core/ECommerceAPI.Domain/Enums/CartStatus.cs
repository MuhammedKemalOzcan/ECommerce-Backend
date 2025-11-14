using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Domain.Enums
{
    public enum CartStatus
    {
        Active = 1,
        Converted = 2,  //Siparişe dönüştürüldü
        Expired = 3, //Süresi doldu
        Merged = 4, //Birleştirildi(giriş yapıp kullanıcıyla birleştirildiğinde)
    }
}
