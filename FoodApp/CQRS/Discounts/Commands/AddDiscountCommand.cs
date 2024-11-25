using FoodApp.Abstraction;
using FoodApp.Data.Entities;
using FoodApp.DTOs;
using FoodApp.Errors;
using MediatR;
using ProjectManagementSystem.Helper;

namespace FoodApp.CQRS.Discounts.Commands
{
    public record AddDiscountCommand (decimal DiscountPercent, DateTime StartDate, DateTime EndDate) : IRequest<Result<int>>;

    public class AddDiscountCommandHandler: BaseRequestHandler<AddDiscountCommand, Result<int>>
    {
        public AddDiscountCommandHandler(RequestParameters requestParameters) : base(requestParameters) { }


        public async override Task<Result<int>> Handle(AddDiscountCommand request, CancellationToken cancellationToken)
        {
            if (request.DiscountPercent <= 0 || request.DiscountPercent > 100)
            {
                return Result.Failure<int>(DiscountErrors.DiscountPercentageNotValid);
            }

            if (request.EndDate <= request.StartDate)
            {
                return Result.Failure<int>(DiscountErrors.DatesNotValid);
            }

            var discount = request.Map<Discount>();

            var discountRepo = _unitOfWork.Repository<Discount>();
            await discountRepo.AddAsync(discount);
            await discountRepo.SaveChangesAsync();

            return Result.Success(discount.Id);
        }
    }
}
