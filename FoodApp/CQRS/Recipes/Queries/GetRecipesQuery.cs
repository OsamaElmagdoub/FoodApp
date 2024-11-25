using FoodApp.Abstraction;
using FoodApp.Data.Entities;
using FoodApp.DTOs;
using FoodApp.Errors;
using FoodApp.Repository.Specification;
using FoodApp.Repository.Specification.RecipeSpec;
using FoodApp.Response;
using MediatR;
using ProjectManagementSystem.Helper;

namespace FoodApp.CQRS.Recipes.Queries;

public record GetRecipesQuery(SpecParams SpecParams) : IRequest<Result<IEnumerable<RecipeToReturnDto>>>;

public class GetRecipesQueryHandler : BaseRequestHandler<GetRecipesQuery, Result<IEnumerable<RecipeToReturnDto>>>
{
    public GetRecipesQueryHandler(RequestParameters requestParameters) : base(requestParameters) { }

    public override async Task<Result<IEnumerable<RecipeToReturnDto>>> Handle(GetRecipesQuery request, CancellationToken cancellationToken)
    {
        var spec = new RecipeSpecification(request.SpecParams);
        var recipe = await _unitOfWork.Repository<Recipe>().GetAllWithSpecAsync(spec);


        if (recipe == null)
        {
            return Result.Failure<IEnumerable<RecipeToReturnDto>>(RecipeErrors.RecipeNotFound);
        }

        var response = recipe.Map<IEnumerable<RecipeToReturnDto>>();

        return Result.Success(response);
    }
}