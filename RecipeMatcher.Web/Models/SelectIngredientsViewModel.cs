using System.ComponentModel.DataAnnotations;

public class SelectIngredientsViewModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = "";// MME: prefer `required` over ` = "";`

    public bool Selected { get; set; } = false;
}