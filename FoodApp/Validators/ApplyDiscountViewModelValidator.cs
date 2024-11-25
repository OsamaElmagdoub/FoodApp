using FluentValidation;
using FoodApp.ViewModels;

namespace FoodApp.Validators
{
    public class ApplyDiscountViewModelValidator :AbstractValidator<ApplyDiscountViewModel>
    {
        public ApplyDiscountViewModelValidator()
        {
            RuleFor(x=>x.DiscountId)
                .NotEmpty().WithMessage("DiscountId Is Required");

            RuleFor(x => x.RecipeId)
                .NotEmpty().WithMessage("RecipetId Is Required");
        }
    }
}
