using FluentValidation;
using FoodApp.ViewModels;

namespace FoodApp.Validators
{
    public class ForgotPasswordViewModelValidator :AbstractValidator<ForgotPasswordViewModel>
    {
        public ForgotPasswordViewModelValidator()
        {

            RuleFor(x => x.Email)
                 .NotEmpty().WithMessage("Email is required");
        }

    }
}
