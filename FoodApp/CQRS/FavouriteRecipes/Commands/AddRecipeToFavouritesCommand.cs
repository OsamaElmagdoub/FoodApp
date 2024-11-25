using FoodApp.Abstraction;
using FoodApp.CQRS.FavouriteRecipes.Queries;
using FoodApp.CQRS.Recipes.Queries;
using FoodApp.CQRS.Users.Queries;
using FoodApp.Data.Entities;
using FoodApp.DTOs;
using FoodApp.Errors;
using MediatR;

namespace FoodApp.CQRS.FavouriteRecipes.Commands
{
    public record AddRecipeToFavouritesCommand(int RecipeId) : IRequest<Result<bool>>;
    public class AddRecipeToFavouritesCommandHandler : BaseRequestHandler<AddRecipeToFavouritesCommand, Result<bool>>
    {
        public AddRecipeToFavouritesCommandHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public override async Task<Result<bool>> Handle(AddRecipeToFavouritesCommand request, CancellationToken cancellationToken)
        {
            var userId = string.IsNullOrEmpty(_userState.ID) ? 0 : int.Parse(_userState.ID); ;
            if (userId == 0)
            {
                return Result.Failure<bool>(UserErrors.UserNotAuthenticated);
            }

            var recipeResult = await _mediator.Send(new GetRecipeByIdQuery(request.RecipeId), cancellationToken);
            if (!recipeResult.IsSuccess)
            {
                return Result.Failure<bool>(recipeResult.Error);
            }
            var recipe = recipeResult.Data;

            var favouriteRecipeResult = await _mediator.Send(new GetFavouriteRecipeByUserIdAndRecipeIdQuery(userId, request.RecipeId), cancellationToken);

            if (favouriteRecipeResult.IsSuccess)
            {
                return Result.Failure<bool>(FavouriteRecipeErrors.FavouriteRecipeAlreadyExists);
            }

            var favouriteRecipe = new FavouriteRecipe
            {
                UserId = userId,
                RecipeId = recipe.Id
            };

            await _unitOfWork.Repository<FavouriteRecipe>().AddAsync(favouriteRecipe);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success(true);
        }
    }
}
