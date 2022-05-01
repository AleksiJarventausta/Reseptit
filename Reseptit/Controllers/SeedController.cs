using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Reseptit.Models;
using Reseptit.Repositories;

namespace Reseptit.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class SeedController:ControllerBase
{
    private readonly IRepository<Recipe> _recipeRepo;

    public SeedController(IRepository<Recipe> recipeRepo)
    {
        _recipeRepo = recipeRepo;
    }

    [HttpPost]
    public async Task<ActionResult> Seed([FromQuery] bool clearExistingData = false)
    {
        if (clearExistingData)
        {
            await _recipeRepo.DeleteManyAsync(s => true );
        }

        var recipes = SeedData.GetRecipes();
        await _recipeRepo.InsertManyAsync(recipes);
        
        return Ok();
    }
}