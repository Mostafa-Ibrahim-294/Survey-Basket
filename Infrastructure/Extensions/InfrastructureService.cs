using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts.Authentication;
using Application.Contracts.Payment;
using Application.Contracts.Repositories;
using Domain.Entites;
using Hangfire;
using Infrastructure.Common.Options;
using Infrastructure.Health;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Infrastructure.Seeders;
using Infrastructure.Services;
using Infrastructure.Services.AuthServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using X.Paymob.CashIn;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace Infrastructure.Extensions
{
    public static class InfrastructureService
    {
        public static void AddInfrastructureServices(this WebApplicationBuilder builder, IConfiguration configuration)
        {
            builder.Services.AddDbContextPool<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddStackExchangeRedisCache(
                options =>
                {
                    options.Configuration = configuration.GetConnectionString("RedisConnection");
                }
            );
            builder.Services.AddHybridCache();
            builder.Services.AddOptions<JwtOptions>()
                .BindConfiguration(nameof(JwtOptions))
                .ValidateDataAnnotations()
                .ValidateOnStart();
            builder.Services.AddHangfire(config =>
                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(configuration.GetConnectionString("HangfireConnection"))
            );
            builder.Services.AddHangfireServer();
            builder.Services.AddHealthChecks()
                .AddSqlServer(configuration.GetConnectionString("DefaultConnection")!)
                .AddHangfire(options =>
                {
                    options.MinimumAvailableServers = 1;
                })
                .AddRedis(configuration.GetConnectionString("RedisConnection")!)
                .AddCheck<MailHealthCheck>("mail service");
            builder.Services.AddScoped<IPollRepository, PollRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>(); // register user repo
            builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
            builder.Services.AddScoped<IVoteRepository, VoteRepository>();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            builder.Services.AddSingleton<IJwtProvider, JwtProvider>();
            builder.Services.AddOptions<MailOptions>()
                .BindConfiguration(nameof(MailOptions))
                .ValidateDataAnnotations()
                .ValidateOnStart();
            builder.Services.AddOptions<PaymobOptions>()
                .BindConfiguration(nameof(PaymobOptions))
                .ValidateDataAnnotations()
                .ValidateOnStart();
            builder.Services.AddScoped<IEmailSender, EmailSender>();
            builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            builder.Services.AddScoped<IUserContext , UserContext>();
            builder.Services.AddScoped<ISeeder, Seeder>();
            builder.Services.AddScoped<IPlanRepository, PlanRepository>();
            builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddPaymobCashIn(config =>
            {
                config.ApiKey = configuration.GetSection(nameof(PaymobOptions)).Get<PaymobOptions>()!.ApiKey;
                config.Hmac = configuration.GetSection(nameof(PaymobOptions)).Get<PaymobOptions>()!.Hmac;
            });
        }
    }
}
