using Microsoft.Extensions.DependencyInjection;
using RecipeMatcher.Web.Data;

namespace RecipeMatcher.Web.Tests;

public class EfReader(IServiceProvider services)
{
    public T Query<T>(Func<AppDbContext, T> query)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        return query(db);
    }
}