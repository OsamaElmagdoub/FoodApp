using FoodApp.Abstraction;
using FoodApp.CQRS.Discounts.Queries;
using FoodApp.CQRS.Recipes.Queries;
using FoodApp.Data.Entities;
using FoodApp.DTOs;
using FoodApp.Errors;
using MediatR;
using ProjectManagementSystem.Helper;

namespace FoodApp.CQRS.Discounts.Commands
{
    public record ApplyDiscountCommand(int RecipeId, int DiscountId) : IRequest<Result<decimal>>;

    public class ApplyDiscountCommandHandler : BaseRequestHandler<ApplyDiscountCommand, Result<decimal>>
    {
        public ApplyDiscountCommandHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public async override Task<Result<decimal>> Handle(ApplyDiscountCommand request, CancellationToken cancellationToken)
        {

            var recipeResult = await _mediator.Send(new GetRecipeByIdQuery(request.RecipeId));
            if (!recipeResult.IsSuccess)
            {
                return Result.Failure<decimal>(RecipeErrors.RecipeNotFound);
            }
            var recipe = recipeResult.Data;


           var activeDiscount = recipe.RecipeDiscounts?
                                     .FirstOrDefault(rd => rd.Discount != null && rd.Discount.IsActive);
            
            if (activeDiscount != null)
            {
                return Result.Failure<decimal>(DiscountErrors.ActiveDiscountAlreadyExists);
            }

            var discountResult = await _mediator.Send(new GetDiscountByIdQuery(request.DiscountId));
            if (!discountResult.IsSuccess)
            {
                return Result.Failure<decimal>(DiscountErrors.DiscountNotFound);
            }
            var discount = discountResult.Data.Map<Discount>();

            recipe.RecipeDiscounts.Add(new RecipeDiscount { RecipeId = recipe.Id, DiscountId = discount.Id });
         
            var recipeRepo = _unitOfWork.Repository<Recipe>();
            recipeRepo.Update(recipe);
            await _unitOfWork.SaveChangesAsync();


            var discountedPrice = recipe.Price - (recipe.Price * (discount.DiscountPercent / 100));

            return Result.Success(discountedPrice);
        }
    }

}
