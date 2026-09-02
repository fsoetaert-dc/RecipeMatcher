using System.ComponentModel.DataAnnotations;

public class IngredientOptionViewModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = "";

    public bool Selected { get; set; } = false;

}