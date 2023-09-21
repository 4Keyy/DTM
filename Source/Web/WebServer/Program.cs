using DTM.Infrastructure.Data;
using DTM.WebServer.Infrastructure;

namespace DTM.WebServer
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddKeyVaultIfConfigured(builder.Configuration);

            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddWebServices();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                await app.InitialiseDatabaseAsync();
            }
            else
            {
                // https://aka.ms/aspnetcore-hsts
                app.UseHsts();
            }

            app.UseHealthChecks("/health");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSwaggerUi3(settings =>
            {
                settings.Path = "/api";
                settings.DocumentPath = "/api/specification.json";
            });

            app.MapControllerRoute(name: "default", pattern: "{controller}/{action=Index}/{id?}");

            app.MapRazorPages();

            app.MapFallbackToFile("index.html");

            app.UseExceptionHandler(options => { }); // ???

            app.MapEndpoints();

            app.Run();
        }
    }
}
