namespace ECommerceAPI.Domain.Exceptions
{
    internal class DomainException : BaseException
    {
        public DomainException(string message) : base(message, 400, "Domain_Error")
        {
        }
    }
}
