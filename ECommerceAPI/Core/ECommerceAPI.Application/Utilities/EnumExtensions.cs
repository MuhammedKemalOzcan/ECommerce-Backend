using ECommerceAPI.Domain.Enums;

namespace ECommerceAPI.Application.Utilities
{
    public static class EnumExtensions
    {
        public static string ToReasonExplanation(this CancelationReason reason)
        {
            return reason switch
            {
                CancelationReason.StoktaYok => "the product is out of stock",
                CancelationReason.HasarliUrun =>
                    "The product could not be shipped because it was found to be damaged during the packaging process.",
                CancelationReason.MusteriTalebi =>
                "Siparişiniz, talebiniz üzerine iptal edilmiştir.",

                CancelationReason.HataliFiyatlandirma =>
                    "Your order has been cancelled due to a system integration error. (Operational Malfunction)",

                CancelationReason.SupheliIslem =>
                    "Due to security procedures, your order has not been confirmed. Please contact your bank.",

                CancelationReason.Diger =>
                    "Your order has been cancelled due to operational reasons. Please contact customer service for more details.",

                _ => "Your order has been canceled."
            };
        }
    }
}