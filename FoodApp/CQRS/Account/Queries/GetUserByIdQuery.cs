using FoodApp.Abstraction;
using FoodApp.Data.Entities;
using FoodApp.DTOs;
using FoodApp.Errors;
using MediatR;

namespace FoodApp.CQRS.Account.Queries
{
    public record GetUserByIdQuery(int UserId) : IRequest<Result<User>>;
    public class GetUserByIdQueryHandler : BaseRequestHandler<GetUserByIdQuery, Result<User>>
    {
        public GetUserByIdQueryHandler(RequestParameters requestParameters) : base(requestParameters)
        {
        }

        public override async Task<Result<User>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Repository<User>().FirstAsync(u => u.Id == request.UserId && !u.IsDeleted);

            if (user == null)
            {
                return Result.Failure<User>(UserErrors.UserNotFound);
            }

            return Result.Success(user);
        }
    }

}
