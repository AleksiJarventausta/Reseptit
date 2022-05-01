using MongoDB.Driver;
using Reseptit.Models;

namespace Reseptit.Repositories;

public class RecipeRepository: MongoRepository<Recipe>
{
    public RecipeRepository(MongoClient client, MongoDbSettings settings, ILogger<Recipe> logger) : base(client, settings, logger)
    {
    }
}