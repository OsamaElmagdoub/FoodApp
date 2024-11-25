using FoodApp.Abstraction;
using FoodApp.Data.Entities;
using FoodApp.DTOs;
using FoodApp.Errors;
using FoodApp.Repository.Specification.CategorySpec;
using FoodApp.Response;
using MediatR;

namespace FoodApp.CQRS.Categories.Queries
{
    public record GetCategoryByIdQuery(int CategoryId) : IRequest<Result<Category>>;

    public record CategoryToReturnDto(int Id, string Name, List<RecipesNamesToReturnDto> Recipes);
    public record RecipesNamesToReturnDto(int Id ,string Name);

    public class GetCategoryByIdQueryHandler : BaseRequestHandler<GetCategoryByIdQuery, Result<Category>>
    {
        public GetCategoryByIdQueryHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public override async Task<Result<Category>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var spec = new CategoryWithRecipesSpecification(request.CategoryId);
            var category = await _unitOfWork.Repository<Category>().GetByIdWithSpecAsync(spec);
            if (category == null)
            {
                return Result.Failure<Category>(CategoryErrors.CategoryNotFound);
            }


            return Result.Success(category);
        }
    }
}
