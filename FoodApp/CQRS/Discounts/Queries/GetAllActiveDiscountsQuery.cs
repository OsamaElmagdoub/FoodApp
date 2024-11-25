using FoodApp.Abstraction;
using FoodApp.Data.Entities;
using FoodApp.DTOs;
using MediatR;
using ProjectManagementSystem.Helper;

namespace FoodApp.CQRS.Discounts.Queries
{
    public record DiscountToReturnDto(int Id, decimal DiscountPercent, DateTime StartDate, DateTime EndDate, DateTime DateCreated) ;
    public record GetAllActiveDiscountsQuery() : IRequest<Result<IEnumerable<DiscountToReturnDto>>>;

    public class GetAllActiveDiscountsQueryHandler : BaseRequestHandler<GetAllActiveDiscountsQuery, Result<IEnumerable<DiscountToReturnDto>>>
    {
        public GetAllActiveDiscountsQueryHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public async override Task<Result<IEnumerable<DiscountToReturnDto>>> Handle(GetAllActiveDiscountsQuery request, CancellationToken cancellationToken)
        {
            var discountRepo = _unitOfWork.Repository<Discount>();
            var allDiscounts = await discountRepo.GetAllAsync();  

            var activeDiscounts = allDiscounts
                .Where(d => d.IsActive) 
                .ToList();

            var mappedDiscount = activeDiscounts.Map<IEnumerable<DiscountToReturnDto>>();

            return Result.Success(mappedDiscount);
        }
    }

}
