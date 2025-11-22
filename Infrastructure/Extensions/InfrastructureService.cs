using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Common.Options;
using Application.Contracts.Authentication;
using Application.Contracts.Repositories;
using Domain.Entites;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Infrastructure.Services.AuthServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using Infrastructure.Seeders;
using Hangfire;
using Infrastructure.Health;

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
                .AddCheck<MailHealthCheck>("mail service");
            builder.Services.AddScoped<IPollRepository, PollRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>(); // register user repo
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            builder.Services.AddSingleton<IJwtProvider, JwtProvider>();
            builder.Services.AddOptions<MailOptions>()
                .BindConfiguration(nameof(MailOptions))
                .ValidateDataAnnotations()
                .ValidateOnStart();
            builder.Services.AddScoped<IEmailSender, EmailSender>();
            builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            builder.Services.AddScoped<IUserContext , UserContext>();
            builder.Services.AddScoped<ISeeder, Seeder>();
        }
    }
}
