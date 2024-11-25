using FluentValidation;
using FoodApp.ViewModels;

namespace FoodApp.Validators
{
    public class AssignRoleToUserViewModelValidator : AbstractValidator<AssignRoleToUserViewModel>
    {
        public AssignRoleToUserViewModelValidator()
        {
            RuleFor(x => x.RoleName)
                 .NotEmpty().WithMessage("RoleName is required");
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId Is required");
        }
    }
}
