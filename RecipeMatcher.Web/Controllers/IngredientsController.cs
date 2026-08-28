using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeMatcher.Web.Data;
using RecipeMatcher.Web.Models;

namespace RecipeMatcher.Web.Controllers;

public class IngredientsController : Controller
{
    private readonly AppDbContext _dbContext;

    public IngredientsController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IActionResult> Index()
    {
        var ingredients = await _dbContext.Ingredients
    .OrderBy(ingredient => ingredient.Name)
    .ToListAsync();

        return View(ingredients);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Ingredient ingredient)
    {
        if (!ModelState.IsValid)
        {
            return View(ingredient);
        }

        _dbContext.Ingredients.Add(ingredient);
        await _dbContext.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var ingredient = await _dbContext.Ingredients.FindAsync(id);

        if (ingredient == null)
        {
            return NotFound();
        }

        return View(ingredient);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Ingredient ingredient)
    {
        if (id != ingredient.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(ingredient);
        }

        var existingIngredient = await _dbContext.Ingredients.FindAsync(id);

        if (existingIngredient == null)
        {
            return NotFound();
        }

        existingIngredient.Name = ingredient.Name;
        await _dbContext.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> DeletePage(int id)
    {
        var ingredient = await _dbContext.Ingredients.FindAsync(id);

        if (ingredient == null)
        {
            return NotFound();
        }

        return View(ingredient);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var ingredient = await _dbContext.Ingredients.FindAsync(id);

        if (ingredient == null)
        {
            return NotFound();
        }

        _dbContext.Ingredients.Remove(ingredient);
        await _dbContext.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}