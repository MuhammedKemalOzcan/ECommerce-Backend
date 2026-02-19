namespace ECommerceAPI.Application.Models
{
    public class ErrorResponse
    {
        public string Message { get; set; }
        public string ErrorCode { get; set; }
        public int StatusCode { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
        public Dictionary<string, string[]>? ValidationErrors { get; set; }
    }
}
