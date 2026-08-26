using System.Net;
using RecipeMatcher.Web.Models;

namespace RecipeMatcher.Web.Tests.Recipes;

public class RecipesTests : IntegrationTest
{
    [Fact]
    public async Task GetRecipesAsyncReturnsRecipesPage()
    {
        Writer.Seed(db => db.Recipes.Add(
            new Recipe
            {
                Id = 1,
                Name = "Spaghetti",
                PreparationMinutes = 45
            }));

        var response = await Client.GetAsync("/recipes");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var html = await response.Content.ReadAsStringAsync();

        Assert.Contains("Spaghetti", html);
        Assert.Contains("45", html);
    }
}