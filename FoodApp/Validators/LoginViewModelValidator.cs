﻿using FluentValidation;
using FoodApp.ViewModels;

namespace FoodApp.Validators
{
    public class LoginViewModelValidator :AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            RuleFor(x => x.Email)
                 .NotEmpty().WithMessage("Email is required");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required");
        }
    }
}