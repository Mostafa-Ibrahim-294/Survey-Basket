using System.Text;
using System.Threading.RateLimiting;
using Api.Middlewares;
using Infrastructure.Common.Options;
using Infrastructure.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace Api.Extensions
{
    public static class PresentationService
    {
        public static void AddPresentationServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>().AddProblemDetails();
            builder.Host.UseSerilog(
                (context, services, configuration) =>
                configuration.ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services)
            );
            var jwtSettings = builder.Configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy =>
                    {
                      policy.AllowAnyHeader()
                            .AllowAnyMethod()
                            .WithOrigins(builder.Configuration.GetSection(ConfigurationConstants.AllowedOrigins).Get<string[]>()!)
                            .AllowCredentials()
                            .SetPreflightMaxAge(TimeSpan.FromMinutes(10));
                    }
                );
            });
            builder.Services.AddRateLimiter(
                options =>
                {
                    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
                    options.AddPolicy(
                        ServiceConstants.UserLimiterPolicy,
                        context => RateLimitPartition.GetFixedWindowLimiter(
                            partitionKey: context.User.Identity?.Name ?? context.Connection.RemoteIpAddress?.ToString() ?? "anonymous",
                            factory: partition => new FixedWindowRateLimiterOptions
                            {
                                PermitLimit = 100,
                                Window = TimeSpan.FromMinutes(1),
                                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                                QueueLimit = 5
                            }
                        )
                    );
                    options.AddConcurrencyLimiter(ServiceConstants.ConcurrentLimiterPolicy, limiterOptions =>
                    {
                        limiterOptions.PermitLimit = 10000;
                        limiterOptions.QueueLimit = 50;
                        limiterOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    });
                }
            );
            builder.Services.AddAuthentication(
                options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }
            ).AddJwtBearer(
                options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtSettings?.SecretKey!)
                        ),
                        ValidIssuer = jwtSettings?.Issuer,
                        ValidAudience = jwtSettings?.Audience,
                    };
                }
            );
            builder.Services.AddAuthorization();
            builder.Services.Configure<IdentityOptions>(
                options =>
                {
                    options.SignIn.RequireConfirmedEmail = true;
                    options.User.RequireUniqueEmail = true;
                } 
            );
        }
    }
}
