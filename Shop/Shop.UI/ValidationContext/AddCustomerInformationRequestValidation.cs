using FluentValidation;
using Shop.Application.Cart;
using Shop.UI.ViewModel.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.UI.ValidationContext
{
    public class AddCustomerInformationRequestValidation
        : AbstractValidator<AddCustomerInformation.Request>
    {
        // Implements the custom rules for validation
        public AddCustomerInformationRequestValidation()
        {
            RuleFor(x => x.FirstName).NotNull();
            RuleFor(x => x.LastName).NotNull();
            RuleFor(x => x.Email).NotNull().EmailAddress();
            RuleFor(x => x.PhoneNumber).NotNull().MinimumLength(7);
            RuleFor(x => x.Address1).NotNull();
            RuleFor(x => x.City).NotNull();
            RuleFor(x => x.PostCode).NotNull();
        }
    }
}
