namespace ECommerceAPI.Domain.Exceptions
{
    public class StockException : BaseException
    {
        public StockException(string message = "Stok Yeterli Değil") : base(message, 400, "INSUFFICIENT_STOCK")
        {
        }
    }
}
