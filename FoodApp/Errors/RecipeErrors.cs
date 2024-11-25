using ProjectManagementSystem.Errors;

namespace FoodApp.Errors;

public class RecipeErrors
{

    public static readonly Error RecipeNotFound =
        new("Recipe is not found", StatusCodes.Status404NotFound);

    public static readonly Error RecipeNotCreated =
    new("An error occurred while creating the recipe", StatusCodes.Status400BadRequest);
}