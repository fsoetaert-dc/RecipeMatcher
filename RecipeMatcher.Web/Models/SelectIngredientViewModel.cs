using System.ComponentModel.DataAnnotations;

namespace RecipeMatcher.Web.Models;

public class SelectIngredientViewModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public required string Name { get; set; }

    public bool Selected { get; set; } = false;
}