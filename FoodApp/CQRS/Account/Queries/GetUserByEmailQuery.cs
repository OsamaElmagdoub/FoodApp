using FoodApp.Abstraction;
using FoodApp.Data.Entities;
using FoodApp.DTOs;
using FoodApp.Errors;
using MediatR;

namespace FoodApp.CQRS.Account.Queries
{
    public record GetUserByEmailQuery(string Email) : IRequest<Result<User>>;

    public class GetUserByEmailQueryHandler : BaseRequestHandler<GetUserByEmailQuery, Result<User>>
    {

        public GetUserByEmailQueryHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public override async Task<Result<User>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Email))
            {
                return Result.Failure<User>(UserErrors.InvalidEmail);
            }

            var user = (await _unitOfWork.Repository<User>().GetAsync(u => u.Email == request.Email)).FirstOrDefault();
            if (user == null)
            {
                return Result.Failure<User>(UserErrors.UserNotFound);
            }

            return Result.Success(user);
        }
    }
}
