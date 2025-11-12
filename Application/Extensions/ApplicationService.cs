using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace Application.Extensions
{
    public static class ApplicationService
    {
        public static void AddApplicationServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(typeof(ApplicationService).Assembly);
            builder.Services.AddValidatorsFromAssembly(typeof(ApplicationService).Assembly)
                .AddFluentValidationAutoValidation();
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationService).Assembly));
            builder.Services.AddHttpContextAccessor();
        }
    }
}