using FoodApp.Abstraction;
using FoodApp.CQRS.Categories.Queries;
using FoodApp.Data.Entities;
using FoodApp.DTOs;
using FoodApp.Errors;
using MediatR;
using ProjectManagementSystem.Helper;

namespace FoodApp.CQRS.Categories.Commands
{
    public record UpdateCategoryCommand(int CategoryId, string Name) : IRequest<Result<bool>>;
    public class UpdateCategoryCommandHandler : BaseRequestHandler<UpdateCategoryCommand, Result<bool>>
    {
        public UpdateCategoryCommandHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public override async Task<Result<bool>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryResult = await _mediator.Send(new GetCategoryByIdQuery(request.CategoryId), cancellationToken);
            if (!categoryResult.IsSuccess)
            {
                return Result.Failure<bool>(CategoryErrors.CategoryNotFound);
            }

            var category = categoryResult.Data;
            category.Name = request.Name;

            _unitOfWork.Repository<Category>().Update(category);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success(true);
        }
    }
}
