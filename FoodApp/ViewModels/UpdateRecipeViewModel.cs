using FoodApp.Abstraction;
using MediatR;

namespace FoodApp.ViewModels;

public record UpdateRecipeViewModel(
    int RecipeId,
    string Name,
    IFormFile ImageUrl,
    decimal Price,
    string Description,
    int CategoryId);