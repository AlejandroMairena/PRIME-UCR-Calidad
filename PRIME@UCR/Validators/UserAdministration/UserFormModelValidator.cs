using FluentValidation;
using PRIME_UCR.Application.DTOs.UserAdministration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Validators.UserAdministration
{
    public class UserFormModelValidator : AbstractValidator<UserFormModel>
    {
        public UserFormModelValidator()
        {
            RuleFor(p => p.Email)
                .NotEmpty()
                .WithMessage("Debe digitar el correo electrónico del usuario a registrar")
                .EmailAddress()
                .WithMessage("Debe digitar un correo electrónico válido");
        }
    }
}
