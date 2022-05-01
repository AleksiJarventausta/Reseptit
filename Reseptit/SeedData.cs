using Reseptit.Models;

namespace Reseptit;

public class SeedData
{
    public static IList<Recipe> GetRecipes()
    {
        return new List<Recipe>()
        {
            new () {Id = Guid.NewGuid()}
        };
    }

}