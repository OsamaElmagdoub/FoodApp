using FoodApp.Abstraction;
using FoodApp.CQRS.Account.Commands;
using FoodApp.CQRS.Account.Queries;
using FoodApp.Data.Entities;
using FoodApp.DTOs;
using FoodApp.Errors;
using MediatR;
using ProjectManagementSystem.Helper;

namespace FoodApp.CQRS.Account.Orchestrator
{
    public record RegisterOrchestrator(
   string UserName,
   string Email,
   string Country,
   string PhoneNumber,
   string Password,
   string ConfirmPassword) : IRequest<Result<bool>>;

    public class RegisterOrchestratorHandler : BaseRequestHandler<RegisterOrchestrator, Result<bool>>
    {
        public RegisterOrchestratorHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public async override Task<Result<bool>> Handle(RegisterOrchestrator request, CancellationToken cancellationToken)
        {
            var command = request.Map<RegisterCommand>();

            var RegisterResult = await _mediator.Send(command);

            if (!RegisterResult.IsSuccess)
            {
                return Result.Failure<bool>(UserErrors.UserDoesntCreated);
            }

           await _mediator.Send(new SendVerificationOTP(request.Email));

            return Result.Success(true);
        }
    }
}
