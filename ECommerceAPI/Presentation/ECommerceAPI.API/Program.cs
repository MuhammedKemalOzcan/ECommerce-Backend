using ECommerceAPI.API.Configurations;
using ECommerceAPI.Application.Settings;
using ECommerceAPI.Infrastructure;
using ECommerceAPI.Infrastructure.Middleware;
using ECommerceAPI.Infrastructure.Services.Storage.Local;
using ECommerceAPI.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NpgsqlTypes;
using Serilog;
using Serilog.Context;
using Serilog.Events;
using Serilog.Sinks.PostgreSQL;
using System.Security.Claims;
using System.Text;

namespace ECommerceAPI.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Log.Information("Uygulama başlatılıyor...");

                var builder = WebApplication.CreateBuilder(args);

                Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(builder.Configuration)
                    .WriteTo.PostgreSQL(
                        connectionString: builder.Configuration.GetConnectionString("PostgreSQL"),
                        tableName: "Logs",
                        needAutoCreateTable: true,
                        restrictedToMinimumLevel: LogEventLevel.Information,
                        columnOptions: new Dictionary<string, ColumnWriterBase>
                        {
                            {"message", new RenderedMessageColumnWriter(NpgsqlDbType.Text)},
                            {"message_template", new MessageTemplateColumnWriter(NpgsqlDbType.Text)},
                            {"level", new LevelColumnWriter(true , NpgsqlDbType.Varchar)},
                            {"time_stamp", new TimestampColumnWriter(NpgsqlDbType.Timestamp)},
                            {"exception", new ExceptionColumnWriter(NpgsqlDbType.Text)},
                            {"log_event", new LogEventSerializedColumnWriter(NpgsqlDbType.Json)},
                            {"username",new  UserColumWriter() }
                        }
                    )
                    .Enrich.FromLogContext()
                    .CreateLogger();

                builder.Host.UseSerilog();

                builder.Services.AddHttpContextAccessor();

                // Add services to the container.
                builder.Services.AddApplicationServices();
                builder.Services.AddPersistenceServices(builder.Configuration);

                var StorageUrlSection = builder.Configuration.GetSection("BaseStorageUrl");
                builder.Services.Configure<StorageUrlSettings>(StorageUrlSection);

                //builder.Services.AddStorage<AzureStorage>();
                builder.Services.AddStorage<LocalStorage>();

                builder.Services.AddInfrastructureServices(builder.Configuration);

                builder.Services.AddCors(options =>
                options.AddPolicy("client", builder =>
                builder.WithOrigins("http://localhost:5173").AllowAnyMethod().AllowAnyHeader().AllowCredentials()
                ));

                // JWT Bearer auth
                var key = builder.Configuration["Jwt:Key"];
                var issuer = builder.Configuration["Jwt:Issuer"];
                var audience = builder.Configuration["Jwt:Audience"];

                builder.Services
                    .AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    }).AddJwtBearer(opt =>
                    {
                        opt.TokenValidationParameters = new()
                        {
                            ValidateIssuer = true,
                            ValidIssuer = issuer,

                            ValidateAudience = true,
                            ValidAudience = audience,

                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!)),

                            ValidateLifetime = true,

                            NameClaimType = ClaimTypes.NameIdentifier,
                            RoleClaimType = ClaimTypes.Role,
                            LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false
                        };
                    });

                builder.Services.AddAuthorization();

                builder.Services.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();


                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }
                app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
                app.UseStaticFiles();
                app.UseHttpsRedirection();
                app.UseCors("client");

                app.UseAuthentication();
                app.UseAuthorization();

                app.Use(async (context, next) =>
                {

                    string username = context.User?.Identity?.IsAuthenticated == true
                    ? context.User.FindFirst(ClaimTypes.Email)?.Value
                    ?? context.User.FindFirst("email")?.Value
                    ?? "Unknown" : "Anonymous";

                    var correlationId = context.Request.Headers["X-Correlation-Id"].FirstOrDefault() ?? context.TraceIdentifier;

                    using (LogContext.PushProperty("user", username))
                    using (LogContext.PushProperty("correlation_id", correlationId))
                    {
                        await next();
                    }
                });

                app.UseSerilogRequestLogging();
                app.MapControllers();

                Log.Information("Uygulama başarıyla başladı!");

                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }


        }
    }
}
