
using Microsoft.Extensions.DependencyInjection;
using RecipeMatcher.Web.Data;

namespace RecipeMatcher.Web.Tests;

public class EfWriter(IServiceProvider services)
{
    public void Seed(Action<AppDbContext> seed)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        seed(db);
        db.SaveChanges();
    }
}