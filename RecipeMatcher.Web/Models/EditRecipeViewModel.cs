using System.ComponentModel.DataAnnotations;

public class EditRecipeViewModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = "";

    [Range(1, 480)]
    public int PreparationMinutes { get; set; }

    public List<IngredientOptionViewModel> Ingredients { get; set; } = [];
}