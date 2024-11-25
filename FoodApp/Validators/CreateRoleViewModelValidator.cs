using FluentValidation;
using FoodApp.ViewModels;

namespace FoodApp.Validators
{
    public class CreateRoleViewModelValidator: AbstractValidator<CreateRoleViewModel>
    {
        public CreateRoleViewModelValidator()
        {
            RuleFor(x => x.RoleName)
                 .NotEmpty().WithMessage("RoleName is required");
        }
    }
}
