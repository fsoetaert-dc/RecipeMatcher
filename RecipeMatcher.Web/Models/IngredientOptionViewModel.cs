using System.ComponentModel.DataAnnotations;
// MME: Remove unused namespaces
using RecipeMatcher.Web.Models;
// MME: Declare Type in a namespace
public class IngredientOptionViewModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = "";

    public bool Selected { get; set; } = false;

}