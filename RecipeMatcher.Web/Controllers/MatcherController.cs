using System.Text.RegularExpressions;
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
            .Select(i => new MatcherViewModel
            {
                Id = i.Id,
                Name = i.Name
            })
            .ToListAsync();

        return View(ingredients);
    }

    [HttpPost]
    public async Task<IActionResult> Search(int[]? ingredientIds)
    {
        

        return View();
    }
}