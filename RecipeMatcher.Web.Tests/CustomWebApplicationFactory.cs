using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RecipeMatcher.Web.Data;
using RecipeMatcher.Web.Tests;

public class CustomWebApplicationFactory
    : WebApplicationFactory<Program>
{
    private SqliteConnection? connection;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {


        builder.UseEnvironment("Testing");



        builder.ConfigureServices(services =>
        {
            connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            // Remove the real service
            services.RemoveAll<AppDbContext>();

            // Add a test implementation
            services.AddDbContext<AppDbContext>(options => options.UseSqlite(connection));

            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.EnsureCreated();
        });
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            connection?.Dispose();
        }

        base.Dispose(disposing);
    }

    public EfReader GetReader() => new(Services);

    public EfWriter GetWriter() => new(Services);
}
