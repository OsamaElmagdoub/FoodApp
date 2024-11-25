using FoodApp.Abstraction;
using FoodApp.CQRS.Account.Queries;
using FoodApp.Data.Entities;
using FoodApp.DTOs;
using FoodApp.Errors;
using MediatR;

namespace FoodApp.CQRS.Account.Commands
{
    public record RevokeTokenCommand(string token) : IRequest<Result<bool>>;

    public class RevokeTokenCommandHandler : BaseRequestHandler<RevokeTokenCommand, Result<bool>>
    {
        public RevokeTokenCommandHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public async override Task<Result<bool>> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
        {
            var userResult = await _mediator.Send(new GetUserByRefreshToken(request.token));

            if (!userResult.IsSuccess)
            {
                return Result.Failure<bool>(UserErrors.InvalidRefreshToken);
            }

            var user = userResult.Data;

            var refreshToken = user.RefreshTokens.FirstOrDefault(rt => rt.Token == request.token);
            if (refreshToken == null || !refreshToken.IsActive)
            {
                return Result.Failure<bool>(UserErrors.InvalidRefreshToken);
            }

            refreshToken.RevokedOn = DateTime.UtcNow;

            var userRepo = _unitOfWork.Repository<User>();

            userRepo.Update(user);
            await userRepo.SaveChangesAsync();

            return Result.Success(true);
        }
    }
}
