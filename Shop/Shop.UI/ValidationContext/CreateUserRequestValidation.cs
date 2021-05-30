using FluentValidation;
using Shop.Application.Users;
using Shop.Domain.Models;
using Shop.UI.ViewModel.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.UI.ValidationContext
{
    public class CreateUserRequestValidation
        : AbstractValidator<CreateUser.Request>
    {
        // Implements the custom rules for validation
        public CreateUserRequestValidation()
        {
            var regex = "^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$";

            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.GstNumber)
                .NotEmpty().When(i => i.IsWholesaleAccount)
                .Length(15).When(i => i.IsWholesaleAccount)
                .Matches(regex).When(i => i.IsWholesaleAccount)
                .WithMessage("Given GST number is not valid. Enter a Valid GST number.").When(i => i.IsWholesaleAccount);
        }
    }
}
