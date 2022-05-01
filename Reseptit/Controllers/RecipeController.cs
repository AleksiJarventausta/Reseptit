using Microsoft.AspNetCore.Mvc;
using Reseptit.Models;
using Reseptit.Repositories;

namespace Reseptit.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipeController
{
    private readonly IRepository<Recipe> _recipeRepo;
    private readonly ILogger<RecipeController> _logger;

    public RecipeController(IRepository<Recipe> recipeRepo, ILogger<RecipeController> logger)
    {
        _recipeRepo = recipeRepo;
        _logger = logger;
    }
    
    
}