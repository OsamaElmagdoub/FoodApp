using FoodApp.Abstraction;
using FoodApp.Data.Entities;
using FoodApp.DTOs;
using FoodApp.Errors;
using MediatR;

namespace FoodApp.CQRS.Roles.Commands
{
    public record RemoveRoleCommand(int RoleId) : IRequest<Result<bool>>;
    public class RemoveRoleCommandHandler : BaseRequestHandler<RemoveRoleCommand, Result<bool>>
    {
        public RemoveRoleCommandHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public override async Task<Result<bool>> Handle(RemoveRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _unitOfWork.Repository<Role>().GetByIdAsync(request.RoleId);
            if (role == null)
            {
                return Result.Failure<bool>(RoleErrors.RoleNotFound);
            }

            _unitOfWork.Repository<Role>().Delete(role);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success(true);
        }
    }
}
