using FoodApp.Abstraction;
using FoodApp.CQRS.Account.Queries;
using FoodApp.Data.Entities;
using FoodApp.DTOs;
using FoodApp.Errors;
using FoodApp.Helper;
using MediatR;
using ProjectManagementSystem.Helper;

namespace FoodApp.CQRS.Account.Commands
{
    public record LoginCommand(string Email, string Password) : IRequest<Result<LoginResponse>>;
    public class LoginResponse()
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }

    public class LoginCommandHandler : BaseRequestHandler<LoginCommand, Result<LoginResponse>>
    {
        public LoginCommandHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public override async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return Result.Failure<LoginResponse>(UserErrors.InvalidCredentials);
            }

            var userResult = await _mediator.Send(new GetUserByEmailQuery(request.Email));

            if (!userResult.IsSuccess)
            {
                return Result.Failure<LoginResponse>(UserErrors.InvalidCredentials);
            }

            var user = userResult.Data;
            if (!PasswordHasher.checkPassword(request.Password, user.PasswordHash) /*|| !user.IsEmailVerified*/)
            {
                return Result.Failure<LoginResponse>(UserErrors.InvalidCredentials);
            }

            var token = TokenGenerator.GenerateToken(user);


            var loginResponse = new LoginResponse()
            {
                Id = user.Id,
                Email = user.Email,
                Token = token,
            };

            var userRepo = _unitOfWork.Repository<User>();
            var refreshTokensResult = await _mediator.Send(new GetUserActiveRefreshTokensQuery(user.Id));

            if (refreshTokensResult.IsSuccess)
            {
                var refreshTokens = refreshTokensResult.Data;
                var activeRefreshToken = user.RefreshTokens.FirstOrDefault(r => r.IsActive);
                loginResponse.RefreshToken = activeRefreshToken.Token;
            }
            else
            {
                var newRefreshToken = TokenGenerator.GenerateRefreshToken();
                user.RefreshTokens.Add(newRefreshToken);
                userRepo.Update(user);
                await userRepo.SaveChangesAsync();
                loginResponse.RefreshToken = newRefreshToken.Token;

            }

            return Result.Success(loginResponse);


        }

    }


}
