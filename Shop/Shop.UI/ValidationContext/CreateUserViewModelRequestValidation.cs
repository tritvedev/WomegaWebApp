using FluentValidation;
using Shop.UI.ViewModel.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.UI.ValidationContext
{
    public class CreateUserViewModelRequestValidation
        : AbstractValidator<CreateUserViewModel>
    {
        // Implements the custom rules for validation
        public CreateUserViewModelRequestValidation()
        {
            RuleFor(x => x.Username).NotNull();
        }
    }
}
