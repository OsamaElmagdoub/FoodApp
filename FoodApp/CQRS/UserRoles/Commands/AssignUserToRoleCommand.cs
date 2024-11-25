using Azure.Core;
using FoodApp.Abstraction;
using FoodApp.CQRS.Account.Queries;
using FoodApp.CQRS.Roles.Commands;
using FoodApp.CQRS.Roles.Queries;
using FoodApp.CQRS.UserRoles.Queries;
using FoodApp.Data.Entities;
using FoodApp.DTOs;
using FoodApp.Errors;
using MediatR;
using System.Threading;

namespace FoodApp.CQRS.UserRoles.Commands
{
    public record AddRoleToUserCommand(int userId, string roleName) : IRequest<Result<bool>>;
    public class AddRoleToUserCommandHandler : BaseRequestHandler<AddRoleToUserCommand, Result<bool>>
    {
        public AddRoleToUserCommandHandler(RequestParameters requestParameters) : base(requestParameters)
        {
        }

        public override async Task<Result<bool>> Handle(AddRoleToUserCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await ValidateUserRole(request.userId,request.roleName);

            if (!validationResult.IsSuccess)
            {
                return Result.Failure<bool>(validationResult.Error);
            }
            var role = validationResult.Data;

            var userRole = new UserRole
            {
                UserId = request.userId,
                RoleId = role.Id
            };

            await _unitOfWork.Repository<UserRole>().AddAsync(userRole);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success(true);
        }
        private async Task<Result<Role>> ValidateUserRole(int userId, string roleName)
        {
            var resultuser = await _mediator.Send(new GetUserByIdQuery(userId));
            if (!resultuser.IsSuccess || resultuser.Data == null)
            {
                return Result.Failure<Role>(UserErrors.UserNotFound);
            }

            var roleResult = await _mediator.Send(new GetRoleByNameQuery(roleName));
            if (!roleResult.IsSuccess || roleResult.Data == null)
            {
                return Result.Failure<Role>(RoleErrors.RoleNotFound);
            }
            var role = roleResult.Data;
            var userRolesResult = await _mediator.Send(new GetUserRoleByIdQuery(userId, role.Id));

            if (userRolesResult.IsSuccess)
            {
                return Result.Failure<Role>(RoleErrors.RoleAlreadyExists);
            }
            return Result.Success(role);
        }
    }
}
