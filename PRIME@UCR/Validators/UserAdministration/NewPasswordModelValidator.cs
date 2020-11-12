using FluentValidation;
using PRIME_UCR.Application.DTOs.UserAdministration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Validators.UserAdministration
{
    public class NewPasswordModelValidator : AbstractValidator<NewPasswordModel>
    {
        public NewPasswordModelValidator()
        {
            RuleFor(p => p.Password)
                .NotEmpty()
                .WithMessage("Digite una contraseña válida");

            RuleFor(p => p.ConfirmedPassword)
                .NotEmpty()
                .WithMessage("Digite su nueva contraseña de nuevo")
                .Must((model, currentPassword) => model.Password == currentPassword)
                .WithMessage("Las contraseñas deben ser iguales");
        }
    }
}
