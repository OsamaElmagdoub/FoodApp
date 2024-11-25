using FoodApp.Abstraction;
using FoodApp.Data.Entities;
using FoodApp.DTOs;
using FoodApp.Errors;
using MediatR;
using ProjectManagementSystem.Helper;

namespace FoodApp.CQRS.Users.Queries
{
    public record GetUserProfileQuery() : IRequest<Result<UserToReturnDto>>;

    public class GetUserProfileQueryHandler : BaseRequestHandler<GetUserProfileQuery, Result<UserToReturnDto>>
    {
        public GetUserProfileQueryHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public async  override Task<Result<UserToReturnDto>> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            var userId = _userState.ID;
            if(string.IsNullOrEmpty(userId))
            {
                return Result.Failure<UserToReturnDto>(UserErrors.NoLoggedInUserFound);

            }
            var user = await _unitOfWork.Repository<User>().GetByIdAsync(int.Parse(userId));

            if (user == null)
            {
                return Result.Failure<UserToReturnDto>(UserErrors.UserNotFound);
            }

            var mappedUser = user.Map<UserToReturnDto>();

            return Result.Success(mappedUser);
        }
    }
}
