using FoodApp.Abstraction;
using FoodApp.Data.Entities;
using FoodApp.DTOs;
using FoodApp.Errors;
using FoodApp.Repository.Specification;
using FoodApp.Repository.Specification.UsesrSpec;
using MediatR;
using ProjectManagementSystem.Helper;

namespace FoodApp.CQRS.Users.Queries
{
    public record GetAllUsersQuery(SpecParams SpecParams) : IRequest<Result<IEnumerable<UserToReturnDto>>>;

    public class UserToReturnDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public DateTime DateCreated { get; set; }
    }
    public class GetAllUsersQuerHandler : BaseRequestHandler<GetAllUsersQuery, Result<IEnumerable<UserToReturnDto>>>
    {
        public GetAllUsersQuerHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public async  override Task<Result<IEnumerable<UserToReturnDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var spec = new UserSpecification(request.SpecParams);
            var users = await _unitOfWork.Repository<User>().GetAllWithSpecAsync(spec);

            if (users == null)
            {
                return Result.Failure<IEnumerable<UserToReturnDto>>(UserErrors.UserNotFound);
            }

            var mappedUser = users.Map<IEnumerable<UserToReturnDto>>();

            return Result.Success(mappedUser);
        }
    }

}
