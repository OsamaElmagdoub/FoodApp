using FoodApp.Abstraction;
using FoodApp.CQRS.Account.Queries;
using FoodApp.Data.Entities;
using FoodApp.DTOs;
using FoodApp.Errors;
using FoodApp.Repository.Interface;
using MediatR;
using ProjectManagementSystem.Helper;

namespace FoodApp.CQRS.Users.Commands
{
    public record ChangePasswordCommand(
    string Email,
    string CurrentPassword,
    string NewPassword) : IRequest<Result<bool>>;


    public class ChangePasswordCommandHandler : BaseRequestHandler<ChangePasswordCommand, Result<bool>>
    {

        public ChangePasswordCommandHandler(RequestParameters requestParameters) : base(requestParameters) { }
        public override async Task<Result<bool>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var userResult = await _mediator.Send(new GetUserByEmailQuery(request.Email));

            if (!userResult.IsSuccess)
            {
                return Result.Failure<bool>(UserErrors.UserNotFound);
            }

            var user = userResult.Data;

            if (!PasswordHasher.checkPassword(request.CurrentPassword, user.PasswordHash))
            {
                return Result.Failure<bool>(UserErrors.InvalidCurrentPassword);
            }

            user.PasswordHash = PasswordHasher.HashPassword(request.NewPassword);

            var userRepo = _unitOfWork.Repository<User>();

            userRepo.Update(user);
            await userRepo.SaveChangesAsync();


            return Result.Success(true);
        }

    }
}

