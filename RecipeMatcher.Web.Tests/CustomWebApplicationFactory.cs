using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RecipeMatcher.Web.Data;

public class CustomWebApplicationFactory
    : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureServices(services =>
        {
            // Remove the real service
            services.RemoveAll<AppDbContext>();

            // Add a test implementation
            services.AddSingleton<AppDbContext>();

            // Example: replace a database registration
            // services.RemoveAll<DbContextOptions<AppDbContext>>();
            // services.AddDbContext<AppDbContext>(options =>
            //     options.UseInMemoryDatabase("TestDatabase"));
        });
    }
}
