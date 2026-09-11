namespace RecipeMatcher.Web.Models;
public class MatchedRecipe
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int PreparationMinutes { get; set; }
    public List<Ingredient> Ingredients { get; set; } = [];
}