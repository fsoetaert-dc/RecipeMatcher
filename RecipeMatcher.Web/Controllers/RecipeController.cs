using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeMatcher.Web.Data;
using RecipeMatcher.Web.Models;

namespace RecipeMatcher.Web.Controllers;

public class RecipesController : Controller
{
    private readonly AppDbContext _dbContext;

    public RecipesController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IActionResult> Index()
    {
        var recipes = await _dbContext.Recipes
    .OrderBy(recipe => recipe.Name)
    .ToListAsync();

        return View(recipes);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Recipe recipe)
    {
        if (!ModelState.IsValid)
        {
            return View(recipe);
        }

        _dbContext.Recipes.Add(recipe);
        await _dbContext.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int id)
    {
        var recipe = await _dbContext.Recipes
            .Include(r => r.RecipeIngredients)
            .ThenInclude(ri => ri.Ingredient)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (recipe is null)
        {
            return NotFound();
        }

        return View(recipe);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var recipe = await _dbContext.Recipes.Include(r => r.RecipeIngredients).SingleOrDefaultAsync(r => r.Id == id);

        if (recipe == null)
        {
            return NotFound();
        }

        var selectedIngredientIds = recipe.RecipeIngredients
            .Select(ri => ri.IngredientId)
            .ToList();

        var ingredients = await _dbContext.Ingredients
            .OrderBy(i => i.Name)
            .Select(i => new IngredientOptionViewModel
            {
                Id = i.Id,
                Name = i.Name,
                Selected = selectedIngredientIds.Contains(i.Id)
            })
            .ToListAsync();

        var model = new EditRecipeViewModel
        {
            Id = recipe.Id,
            Name = recipe.Name,
            PreparationMinutes = recipe.PreparationMinutes,
            Ingredients = ingredients
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, EditRecipeViewModel editRecipeViewModel, int[] ingredientIds)
    {
        if (id != editRecipeViewModel.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(editRecipeViewModel);
        }

        var existingRecipe = await _dbContext.Recipes
        .Include(r => r.RecipeIngredients)
        .SingleOrDefaultAsync(r => r.Id == id);

        if (existingRecipe == null)
        {
            return NotFound();
        }

        existingRecipe.Name = editRecipeViewModel.Name;
        existingRecipe.PreparationMinutes = editRecipeViewModel.PreparationMinutes;

        existingRecipe.RecipeIngredients.Clear();

        foreach (var ingredientId in ingredientIds)
        {
            var ri = new RecipeIngredient
            {
                RecipeId = id,
                IngredientId = ingredientId
            };
            existingRecipe.RecipeIngredients.Add(ri);
        }

        await _dbContext.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> DeletePage(int id)
    {
        var recipe = await _dbContext.Recipes.FindAsync(id);

        if (recipe == null)
        {
            return NotFound();
        }

        return View(recipe);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var recipe = await _dbContext.Recipes.FindAsync(id);

        if (recipe == null)
        {
            return NotFound();
        }

        _dbContext.Recipes.Remove(recipe);
        await _dbContext.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}