using FluentValidation;
using FoodApp.ViewModels;

namespace FoodApp.Validators
{
    public class VerifyViewModelValidator :AbstractValidator<VerifyViewModel>
    {
        public VerifyViewModelValidator()
        {
            RuleFor(x=>x.Email).NotEmpty().WithMessage("Email is required");

            RuleFor(x => x.OTP).NotEmpty().WithMessage("OTP is required");
        }
    }
}
