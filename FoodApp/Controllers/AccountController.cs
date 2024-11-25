using FoodApp.Abstraction;
using FoodApp.CQRS.Account.Commands;
using FoodApp.CQRS.Account.Orchestrator;
using FoodApp.DTOs;
using FoodApp.Errors;
using FoodApp.Helper;
using FoodApp.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Helper;

namespace FoodApp.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(ControllerParameters controllerParameters) : base(controllerParameters) { }

        [HttpPost("Register")]
        public async Task<Result> Register(RegisterViewModel viewModel)
        {
            var command = viewModel.Map<RegisterOrchestrator>();
            var result= await _mediator.Send(command);
            return result;

        }
        [HttpPost("VerifyAccount")]
        public async Task<Result<bool>> Verify(VerifyViewModel viewModel)
        {
            var command = viewModel.Map<VerifyOTPCommand>();
            var result = await _mediator.Send(command);
            return result;

        }
        [HttpPost("ResendVerificationCode")]
        public async Task<Result<bool>> ResendVerificationCode(string email)
        {
            var result = await _mediator.Send(new SendVerificationOTP(email));
            return result;

        }

        [HttpPost("Login")]
        public async Task<Result<LoginResponse>> Login(LoginViewModel viewModel)
        {
            var command = viewModel.Map<LoginCommand>();
            var result = await _mediator.Send(command);
            if (result.Data == null || string.IsNullOrEmpty(result.Data.RefreshToken))
            {
                return Result.Failure<LoginResponse>(UserErrors.InvalidCredentials);
            }
            CookieHelper.SetRefreshTokenCookie(Response, result.Data.RefreshToken);
            return result;

        }

        [HttpPost("RefreshToken")]
        public async Task<Result<LoginResponse>> RefreshToken()
        {
            var refreshToken = CookieHelper.GetRefreshTokenCookie(Request);
            var result = await _mediator.Send(new RefreshTokenCommand(refreshToken));
            if(!result.IsSuccess)
                return Result.Failure<LoginResponse>(UserErrors.InvalidRefreshToken);
            CookieHelper.SetRefreshTokenCookie(Response, result.Data.RefreshToken);

            return result;

        }

        [HttpPost("RevokeToken")]
        public async Task<Result<bool>> RevokeToken(string? token)
        {
            var result = await _mediator.Send(new RevokeTokenCommand(token ?? Request.Cookies["refreshToken"]));
            if(string.IsNullOrEmpty(token))
                return Result.Failure<bool>(UserErrors.TokenIsRequired);
            return result;
        }


        [HttpPost("ForgotPassword")]
        public async Task<Result<bool>> ForgotPassword(ForgotPasswordViewModel viewModel)
        {
            var command = viewModel.Map<ForgotPasswordCommand>();

            var response = await _mediator.Send(command);

            return response;
        }

        [HttpPost("ResetPassword")]
        public async Task<Result<bool>> ResetPassword(ResetPasswordViewModel viewModel)
        {
            var command = viewModel.Map<ResetPasswordCommand>();
            var response = await _mediator.Send(command);

            return response;
        }


    }
}
