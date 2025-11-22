
using Api.Extensions;
using Application.Extensions;
using Hangfire;
using Infrastructure.Extensions;
using Infrastructure.Seeders;
using Serilog;

namespace Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.AddInfrastructureServices(builder.Configuration);
            builder.AddApplicationServices();
            builder.AddPresentationServices();



            var app = builder.Build();
            app.UseExceptionHandler();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseHangfireDashboard();
            }
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var seeder = services.GetRequiredService<ISeeder>();
                await seeder.SeedAsync();
            }

            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();

            app.UseCors();
            app.MapHealthChecks("/health");
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
