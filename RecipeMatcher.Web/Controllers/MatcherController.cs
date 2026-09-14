using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeMatcher.Web.Data;
using RecipeMatcher.Web.Models;

namespace RecipeMatcher.Web.Controllers;

public class MatcherController : Controller
{
    private readonly AppDbContext _dbContext;

    public MatcherController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var ingredients = await _dbContext.Ingredients
            .OrderBy(i => i.Name)
            .Select(i => new SelectIngredientViewModel
            {
                Id = i.Id,
                Name = i.Name
            })
            .ToListAsync();

        return View(ingredients);
    }

    [HttpPost]
    public async Task<IActionResult> Results(int[]? ingredientIds)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction(nameof(Index));
        }

        if (ingredientIds == null || ingredientIds.Length == 0)
        {
            return BadRequest();
        }

        var recipes = await _dbContext.Recipes
            .Include(r => r.RecipeIngredients)
            .ThenInclude(ri => ri.Ingredient)
            .Where(r => r.RecipeIngredients
                .Any(ri => ingredientIds
                    .Contains(ri.IngredientId)))
            .ToListAsync();

        var  matchedRecipesViewModel = new MatchedRecipesViewModel
        {
            Name = "Results",
            SelectedIngredients = await _dbContext.Ingredients
                .Where(i => ingredientIds
                    .Contains(i.Id))
                .ToListAsync(),
            MatcherRecipes = recipes
        };

        return View(matchedRecipesViewModel);
    }
}