using System.Net;
using System.Net.Http.Json;
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

    [Fact]
    public async Task PostEmptyNameRecipeAsyncShowsNameRequired()
    {
        var request =
            new Recipe
            {
                Id = 1,
                Name = "",
                PreparationMinutes = 45
            };

        var response = await Client.PostAsJsonAsync("/recipes/create", request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var html = await response.Content.ReadAsStringAsync();

        Assert.Contains("The Name field is required.", html);
        Assert.Contains("Create", html);
    }

    [Fact]

    public async Task GetDetailsAsyncReturnsOK()
    {
        Writer.Seed(db => db.Recipes.Add(
            new Recipe
            {
                Id = 1,
                Name = "Lasagna",
                PreparationMinutes = 45
            }));

        var id =  1;
        var response = await Client.GetAsync($"/recipes/Details/{id}");
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var html = await response.Content.ReadAsStringAsync();

        Assert.Contains("Lasagna", html);
        Assert.Contains("45", html);
    }

        [Fact]

        public async Task GetDetailsAsyncReturnsNotFoundWhenIdDoesntExist()
    {
        Writer.Seed(db => db.Recipes.Add(
            new Recipe
            {
                Id = 1,
                Name = "Lasagna",
                PreparationMinutes = 45
            }));

        var id =  15;
        var response = await Client.GetAsync($"/recipes/Details/{id}");
        
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

    }
}