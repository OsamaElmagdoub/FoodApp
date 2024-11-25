using FoodApp.Abstraction;
using FoodApp.Data.Entities;
using FoodApp.DTOs;
using FoodApp.Errors;
using MediatR;

namespace FoodApp.CQRS.FavouriteRecipes.Queries
{
    public record GetFavouriteRecipeByUserIdAndRecipeIdQuery(int UserId, int RecipeId) : IRequest<Result<FavouriteRecipe>>;
    public class GetFavouriteRecipeByUserIdAndRecipeIdQueryHandler : BaseRequestHandler<GetFavouriteRecipeByUserIdAndRecipeIdQuery, Result<FavouriteRecipe>>
    {
        public GetFavouriteRecipeByUserIdAndRecipeIdQueryHandler(RequestParameters requestParameters) : base(requestParameters)
        {
        }

        public override async Task<Result<FavouriteRecipe>> Handle(GetFavouriteRecipeByUserIdAndRecipeIdQuery request, CancellationToken cancellationToken)
        {
            var favouriteRecipe =( await _unitOfWork.Repository<FavouriteRecipe>()
                .GetAsync(fr => fr.UserId == request.UserId && fr.RecipeId == request.RecipeId && fr.IsDeleted == false)).FirstOrDefault();
            if (favouriteRecipe == null)
            {
                return Result.Failure<FavouriteRecipe>(FavouriteRecipeErrors.FavouriteRecipeNotFound);
            }

            return Result.Success(favouriteRecipe);

        }
    }

}
