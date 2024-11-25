using FoodApp.Abstraction;
using FoodApp.Data.Entities;
using FoodApp.Repository.Interface;
using FoodApp.Repository.Specification;
using FoodApp.Repository.Specification.RecipeSpec.RecipeSpec;
using MediatR;

namespace FoodApp.CQRS.Recipes.Queries;

public record GetRecipesCountQuery(SpecParams SpecParams) : IRequest<Result<int>>;

public class GetRecipesCountQueryHandler : IRequestHandler<GetRecipesCountQuery, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetRecipesCountQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<int>> Handle(GetRecipesCountQuery request, CancellationToken cancellationToken)
    {
        var RecipeSpec = new CountRecipeWithSpec(request.SpecParams);
        var count = await _unitOfWork.Repository<Recipe>().GetCountWithSpecAsync(RecipeSpec);

        return Result.Success(count);
    }
}