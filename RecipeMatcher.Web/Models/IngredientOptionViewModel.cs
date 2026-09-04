using System.ComponentModel.DataAnnotations;
using RecipeMatcher.Web.Models;

public class IngredientOptionViewModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = "";

    public bool Selected { get; set; } = false;

}