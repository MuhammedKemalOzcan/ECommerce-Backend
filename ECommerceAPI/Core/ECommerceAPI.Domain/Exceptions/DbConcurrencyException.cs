namespace ECommerceAPI.Domain.Exceptions
{
    public class DbConcurrencyException : BaseException
    {
        public DbConcurrencyException(string message, int statusCode, string errorCode) : base(message, 409, "Data_Conflict")
        {
        }
    }
}
