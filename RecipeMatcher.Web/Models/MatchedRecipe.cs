using RecipeMatcher.Web.Models;
// MME: Declare Type in a namespace
public class MatchedRecipe
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int PreparationMinutes { get; set; }
    public List<Ingredient> Ingredients { get; set; } = [];
}