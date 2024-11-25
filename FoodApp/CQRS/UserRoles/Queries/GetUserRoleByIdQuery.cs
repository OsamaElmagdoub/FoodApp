using FoodApp.Abstraction;
using FoodApp.CQRS.UserRoles.Commands;
using FoodApp.Data.Entities;
using FoodApp.DTOs;
using FoodApp.Errors;
using MediatR;

namespace FoodApp.CQRS.UserRoles.Queries
{
    public record GetUserRoleByIdQuery(int UserId, int RoleId) : IRequest<Result<UserRole>>;
    public class GetUserRoleByIdQueryHandler : BaseRequestHandler<GetUserRoleByIdQuery, Result<UserRole>>
    {
        public GetUserRoleByIdQueryHandler(RequestParameters requestParameters) : base(requestParameters)
        {
        }

        public override async Task<Result<UserRole>> Handle(GetUserRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var UserRoles = (await _unitOfWork.Repository<UserRole>()
          .GetAsync(ur => ur.UserId == request.UserId && ur.RoleId == request.RoleId && !ur.IsDeleted)).FirstOrDefault();

            if (UserRoles == null)
            {
                return Result.Failure<UserRole>(RoleErrors.UserNotAssignedToThatRole);
            }

            return Result.Success(UserRoles);
        }
    }
}
