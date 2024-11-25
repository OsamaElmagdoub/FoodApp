using FoodApp.Abstraction;
using FoodApp.Data.Entities;
using FoodApp.DTOs;
using FoodApp.Errors;
using MediatR;

namespace FoodApp.CQRS.Roles.Queries
{
    public record GetRoleByNameQuery(string Name) : IRequest<Result<Role>>;
    public class GetRoleByNameQueryHandler : BaseRequestHandler<GetRoleByNameQuery, Result<Role>>
    {
        public GetRoleByNameQueryHandler(RequestParameters requestParameters) : base(requestParameters) { }
        public override async Task<Result<Role>> Handle(GetRoleByNameQuery request, CancellationToken cancellationToken)
        {
            var role = (await _unitOfWork.Repository<Role>().GetAsync(u => u.Name == request.Name)).FirstOrDefault();
            if (role == null)
            {
                return null;
            }

            return Result.Success(role);
        }
    }
}
