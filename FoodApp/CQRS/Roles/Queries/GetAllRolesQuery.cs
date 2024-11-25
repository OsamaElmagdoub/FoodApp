using FoodApp.Abstraction;
using FoodApp.Data.Entities;
using FoodApp.DTOs;
using MediatR;

namespace FoodApp.CQRS.Roles.Queries
{
    public record GetAllRolesQuery() : IRequest<Result<List<Role>>>;
    public class GetAllRolesQueryHandler : BaseRequestHandler<GetAllRolesQuery, Result<List<Role>>>
    {
        public GetAllRolesQueryHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public override async Task<Result<List<Role>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _unitOfWork.Repository<Role>().GetAllAsync(); 
            return Result.Success(roles.ToList());
        }
    }
}
