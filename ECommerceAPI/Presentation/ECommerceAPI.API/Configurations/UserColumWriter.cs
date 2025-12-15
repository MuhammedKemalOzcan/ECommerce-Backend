using NpgsqlTypes;
using Serilog.Events;
using Serilog.Sinks.PostgreSQL;

namespace ECommerceAPI.API.Configurations
{
    public class UserColumWriter : ColumnWriterBase
    {
        public UserColumWriter() : base(NpgsqlDbType.Varchar)
        {
        }
        public override object GetValue(LogEvent logEvent, IFormatProvider formatProvider = null)
        {
            var user = logEvent.Properties.FirstOrDefault(p => p.Key == "username").Value?.ToString();
            return user != null ? user.Trim('"') : "Anonymous";
        }
    }
}
