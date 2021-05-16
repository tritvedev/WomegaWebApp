using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Shop.Application.Users;
using Shop.Domain.Models;
using Shop.UI.ViewModel.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.UI.ValidationContext
{
    public class CreateUserViewModelRequestValidation
        : AbstractValidator<CreateUser.Request>
    {
        // Implements the custom rules for validation
        public CreateUserViewModelRequestValidation()
        {
            var regex = "^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$"; 
            RuleFor(x => x.UserName).NotNull();
            RuleFor(x => x.GstNumber)
                .NotEmpty()
                .Length(15)
                .Matches(regex)
                .WithMessage("Given GST number is not valid. Enter a Valid GST number.");
        }
    }
}
