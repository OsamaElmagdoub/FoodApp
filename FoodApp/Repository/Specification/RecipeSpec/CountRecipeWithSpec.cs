using FoodApp.Data.Entities;

namespace FoodApp.Repository.Specification.RecipeSpec.RecipeSpec;

public class CountRecipeWithSpec : BaseSpecification<Recipe>
{
    public CountRecipeWithSpec(SpecParams specParams)
        : base(p => !p.IsDeleted)
    {

    }
}