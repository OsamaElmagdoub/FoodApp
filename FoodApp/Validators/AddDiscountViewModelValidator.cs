using FluentValidation;
using FoodApp.ViewModels;

namespace FoodApp.Validators
{
    public class AddDiscountViewModelValidator : AbstractValidator<AddDiscountViewModel>
    {
        public AddDiscountViewModelValidator()
        {
            RuleFor(x => x.DiscountPercent)
                .NotEmpty().WithMessage("DiscountPercent is required");

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("StartDate is required");

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("EndDate is required");
        }
    }   
}
