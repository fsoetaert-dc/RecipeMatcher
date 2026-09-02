using System.ComponentModel.DataAnnotations;

namespace RecipeMatcher.Web.Models;

public class Ingredient
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = "";

    public ICollection<RecipeIngredient> RecipeIngredients { get; set; }
    = new List<RecipeIngredient>();
}