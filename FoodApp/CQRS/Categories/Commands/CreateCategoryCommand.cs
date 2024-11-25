using FoodApp.Abstraction;
using FoodApp.CQRS.Categories.Queries;
using FoodApp.Data.Entities;
using FoodApp.DTOs;
using FoodApp.Errors;
using MediatR;
using ProjectManagementSystem.Helper;

namespace FoodApp.CQRS.Categories.Commands
{
    public record CreateCategoryCommand(string Name) : IRequest<Result<int>>;
    public class CreateCategoryCommandHandler : BaseRequestHandler<CreateCategoryCommand, Result<int>>
    {
        public CreateCategoryCommandHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public override async Task<Result<int>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var existingCategory = await _mediator.Send(new GetCategoryByNameQuery(request.Name), cancellationToken);
            if (existingCategory.IsSuccess)
            {
                return Result.Failure<int>(CategoryErrors.CategoryAlreadyExists);
            }

            var category = request.Map<Category>();

            await _unitOfWork.Repository<Category>().AddAsync(category);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success(category.Id);
        }
    }
}
