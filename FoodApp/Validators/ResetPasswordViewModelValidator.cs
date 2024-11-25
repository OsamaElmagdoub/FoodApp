using FluentValidation;
using FoodApp.ViewModels;

namespace FoodApp.Validators
{
    public class ResetPasswordViewModelValidator :AbstractValidator<ResetPasswordViewModel>
    {
        public ResetPasswordViewModelValidator()
        {
            RuleFor(x => x.Email)
                 .NotEmpty().WithMessage("Email is required");

            RuleFor(x => x.OTP)
                .NotEmpty().WithMessage("OTP is required");

            RuleFor(x => x.NewPassword)
                 .NotEmpty().WithMessage("Password is required");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm Password is required");
        }
    }
}
