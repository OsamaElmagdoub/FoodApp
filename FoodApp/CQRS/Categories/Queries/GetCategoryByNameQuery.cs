using FoodApp.Abstraction;
using FoodApp.Data.Entities;
using FoodApp.DTOs;
using FoodApp.Errors;
using MediatR;

namespace FoodApp.CQRS.Categories.Queries
{
    public record GetCategoryByNameQuery(string Name) : IRequest<Result<Category>>;
    public class GetCategoryByNameQueryHandler : BaseRequestHandler<GetCategoryByNameQuery, Result<Category>>
    {
        public GetCategoryByNameQueryHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public override async Task<Result<Category>> Handle(GetCategoryByNameQuery request, CancellationToken cancellationToken)
        {
            var category = (await _unitOfWork.Repository<Category>().GetAsync(c => c.Name == request.Name)).FirstOrDefault();
            if (category == null)
            {
                return Result.Failure<Category>(CategoryErrors.CategoryNotFound);
            }

            return Result.Success(category);
        }
    }

}
