namespace RecipeMatcher.Web.Models;

public class MatchedRecipesViewModel
{
    public int Id {get;set;}
    public required string Name = "Found Recipes";
    public List<Ingredient> SelectedIngredients {get; set;} = [];
    public List<Recipe> MatcherRecipes {get; set;} = [];
}