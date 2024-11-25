using FluentValidation;
using FoodApp.ViewModels;

namespace FoodApp.Validators
{
    public class RegisterViewModelValidator :AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator()
        {
            RuleFor(x => x.UserName)
                 .NotEmpty().WithMessage("UserName is required");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("A valid email is required");

            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Country is required");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("PhoneNumber is required");

            //RuleFor(x => x.Password)
            //    .NotEmpty().WithMessage("Password is required.")
            //    .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")
            //    .WithMessage("Password must be at least 8 characters long, and include at least one uppercase letter, one lowercase letter, one digit, and one special character");

            RuleFor(x => x.ConfirmPassword)
             .NotEmpty().WithMessage("Password is required.");

        }
    }
}
