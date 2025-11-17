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
            builder.Services.AddOptions<JwtOptions>()
                .BindConfiguration(nameof(JwtOptions))
                .ValidateDataAnnotations()
                .ValidateOnStart();

            builder.Services.AddScoped<IPollRepository, PollRepository>();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            builder.Services.AddSingleton<IJwtProvider, JwtProvider>();
            builder.Services.AddOptions<MailOptions>()
                .BindConfiguration(nameof(MailOptions))
                .ValidateDataAnnotations()
                .ValidateOnStart();
            builder.Services.AddScoped<IEmailSender, EmailSender>();

        }
    }
