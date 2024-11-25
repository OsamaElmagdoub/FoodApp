using FoodApp.Abstraction;
using FoodApp.Data.Entities;
using FoodApp.DTOs;
using FoodApp.Errors;
using FoodApp.Repository.Specification.DiscountSpec;
using MediatR;
using ProjectManagementSystem.Helper;

namespace FoodApp.CQRS.Discounts.Queries
{
    public record GetDiscountByIdQuery(int DiscountId) : IRequest<Result<Discount>>;

    public class GetDiscountByIdQueryHandler : BaseRequestHandler<GetDiscountByIdQuery, Result<Discount>>
    {
        public GetDiscountByIdQueryHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public async override Task<Result<Discount>> Handle(GetDiscountByIdQuery request, CancellationToken cancellationToken)
        {
            var discountRepo = _unitOfWork.Repository<Discount>();
            var spec = new DiscountSpecification(request.DiscountId);
            var discount = await discountRepo.GetByIdWithSpecAsync(spec);

            if (discount == null)
            {
                return Result.Failure<Discount>(DiscountErrors.DiscountNotFound);
            }
            if (!discount.IsActive)
            {
                return Result.Failure<Discount>(DiscountErrors.DiscoutNotActive); 
            }

            return Result.Success(discount);
        }
    }

}
