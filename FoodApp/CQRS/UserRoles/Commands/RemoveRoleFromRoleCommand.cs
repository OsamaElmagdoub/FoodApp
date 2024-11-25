using FoodApp.Abstraction;
using FoodApp.CQRS.Account.Queries;
using FoodApp.CQRS.Roles.Queries;
using FoodApp.Data.Entities;
using FoodApp.DTOs;
using FoodApp.Errors;
using MediatR;

namespace FoodApp.CQRS.UserRoles.Commands
{
    public record RemoveRoleFromUserCommand(int UserId, int RoleId) : IRequest<Result<bool>>;
    public class RemoveRoleFromUserCommandHandler : BaseRequestHandler<RemoveRoleFromUserCommand, Result<bool>>
    {
        public RemoveRoleFromUserCommandHandler(RequestParameters requestParameters) : base(requestParameters)
        {
        }

        public override async Task<Result<bool>> Handle(RemoveRoleFromUserCommand request, CancellationToken cancellationToken)
        {
            var roleResult = await _mediator.Send(new GetRoleByIdQuery(request.RoleId));
            if (!roleResult.IsSuccess)
            {
                return Result.Failure<bool>(RoleErrors.RoleNotFound);
            }
            var role = roleResult.Data;

            var userResult = await _mediator.Send(new GetUserByIdQuery(request.UserId));
            if (!userResult.IsSuccess)
            {
                return Result.Failure<bool>(UserErrors.UserNotFound);
            }
            var user = userResult.Data;


            var userRole = (await _unitOfWork.Repository<UserRole>()
                .GetAsync(ur => ur.UserId == request.UserId && ur.RoleId == request.RoleId)).FirstOrDefault();

            if (userRole == null)
            {
                return Result.Failure<bool>(RoleErrors.RoleNotAssigned);
            }

            _unitOfWork.Repository<UserRole>().Delete(userRole);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success(true);
        }
    }
}
