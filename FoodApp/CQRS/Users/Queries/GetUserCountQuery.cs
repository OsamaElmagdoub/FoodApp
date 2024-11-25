using FoodApp.Abstraction;
using FoodApp.Data.Entities;
using FoodApp.Repository.Interface;
using FoodApp.Repository.Specification.UsesrSpec;
using FoodApp.Repository.Specification;
using MediatR;
using FoodApp.DTOs;

namespace FoodApp.CQRS.Users.Queries
{
    public record GetUserCountQuery(SpecParams SpecParams) : IRequest<Result<int>>;

    public class GetUserCountQueryHandler : BaseRequestHandler<GetUserCountQuery, Result<int>>
    {
        public GetUserCountQueryHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public async override Task<Result<int>> Handle(GetUserCountQuery request, CancellationToken cancellationToken)
        {
            var userSpec = new CountUserWithSpec(request.SpecParams);
            var count = await _unitOfWork.Repository<User>().GetCountWithSpecAsync(userSpec);

            return Result.Success(count);
        }
    }
}
