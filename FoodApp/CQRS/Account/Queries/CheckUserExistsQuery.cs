using FoodApp.Abstraction;
using FoodApp.CQRS;
using FoodApp.Data.Entities;
using FoodApp.DTOs;
using MediatR;


namespace FoodApp.CQRS.Account.Queries
{
    public record CheckUserExistsQuery(string UserName, string Email) : IRequest<Result<bool>>;

    public class CheckUserExistsQueryHandler : BaseRequestHandler<CheckUserExistsQuery, Result<bool>>
    {
        public CheckUserExistsQueryHandler(RequestParameters requestParameters) : base(requestParameters) { }
        public override async Task<Result<bool>> Handle(CheckUserExistsQuery request, CancellationToken cancellationToken)
        {
            var existingUser = await _unitOfWork.Repository<User>()
                            .GetAsync(u => u.Email == request.Email || u.UserName == request.UserName);

            return Result.Success(existingUser.Any());
        }
    }
}
