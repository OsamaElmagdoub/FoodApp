using FoodApp.Abstraction;
using FoodApp.CQRS.Account.Queries;
using FoodApp.Data.Entities;
using FoodApp.DTOs;
using FoodApp.Errors;
using FoodApp.Helper;
using MediatR;

namespace FoodApp.CQRS.Account.Commands
{
    public record RefreshTokenCommand(string token) : IRequest<Result<LoginResponse>>;

    public class RefreshTokenCommandHandler : BaseRequestHandler<RefreshTokenCommand, Result<LoginResponse>>
    {
        public RefreshTokenCommandHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public async override Task<Result<LoginResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var userResult = await _mediator.Send(new GetUserByRefreshToken(request.token));

            if (!userResult.IsSuccess)
            {
                return Result.Failure<LoginResponse>(UserErrors.InvalidRefreshToken);
            }

            var user = userResult.Data;

            var refreshToken = user.RefreshTokens.FirstOrDefault(rt => rt.Token == request.token);
            if (refreshToken == null || !refreshToken.IsActive)
            {
                return Result.Failure<LoginResponse>(UserErrors.InvalidRefreshToken);
            }

            refreshToken.RevokedOn = DateTime.UtcNow;

            var newRefreshToken = TokenGenerator.GenerateRefreshToken();
            user.RefreshTokens.Add(newRefreshToken);
            var userRepo = _unitOfWork.Repository<User>();
            userRepo.Update(user);
            await userRepo.SaveChangesAsync();

            var jwtToken = TokenGenerator.GenerateToken(user);

            var loginResponse = new LoginResponse
            {
                Id = user.Id,
                Email = user.Email,
                Token = jwtToken,
                RefreshToken = newRefreshToken.Token
            };

            return Result.Success(loginResponse);

        }
    }
}
