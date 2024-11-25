using FoodApp.Abstraction;
using FoodApp.CQRS.FavouriteRecipes.Queries;
using FoodApp.Data.Entities;
using FoodApp.DTOs;
using FoodApp.Errors;
using MediatR;

namespace FoodApp.CQRS.FavouriteRecipes.Commands
{
    public record RemoveRecipeFromFavouritesCommand(int RecipeId) : IRequest<Result<bool>>;
    public class RemoveRecipeFromFavouritesCommandHandler : BaseRequestHandler<RemoveRecipeFromFavouritesCommand, Result<bool>>
    {
        public RemoveRecipeFromFavouritesCommandHandler(RequestParameters requestParameters) : base(requestParameters)
        {
        }

        public override async Task<Result<bool>> Handle(RemoveRecipeFromFavouritesCommand request, CancellationToken cancellationToken)
        {
            var userId = string.IsNullOrEmpty(_userState.ID) ? 0 : int.Parse(_userState.ID);
            if (userId == 0)
            {
                return Result.Failure<bool>(UserErrors.UserNotAuthenticated);
            }
            var favouriteRecipeResult = await _mediator.Send(new GetFavouriteRecipeByUserIdAndRecipeIdQuery(userId, request.RecipeId), cancellationToken);

            if (!favouriteRecipeResult.IsSuccess)
            {
                return Result.Failure<bool>(FavouriteRecipeErrors.FavouriteRecipeNotFound);
            }

            var favouriteRecipe = favouriteRecipeResult.Data;
            _unitOfWork.Repository<FavouriteRecipe>().Delete(favouriteRecipe);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success(true);
        }
    }
}
