using FoodApp.Abstraction;
using FoodApp.Data.Entities;
using MediatR;

namespace FoodApp.ViewModels;
public record CreateRecipeViewModel(
    string Name,
    IFormFile ImageUrl,
    decimal Price,
    string Description,
    int CategoryId);