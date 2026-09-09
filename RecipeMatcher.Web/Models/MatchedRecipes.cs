using RecipeMatcher.Web.Models;

public class MatchedRecipes
{
    public int Id { get; set;} 
    public string Name { get; set;} = "";
    public List<Ingredient> Ingredients { get; set;} = [];
}