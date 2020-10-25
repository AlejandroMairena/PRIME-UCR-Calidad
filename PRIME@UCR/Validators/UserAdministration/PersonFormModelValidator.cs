using FluentValidation;
using PRIME_UCR.Application.DTOs.UserAdministration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Validators.UserAdministration
{
    public class PersonFormModelValidator : AbstractValidator<PersonFormModel>
    {
        public PersonFormModelValidator()
        {
            RuleFor(p => p.IdCardNumber)
                .NotEmpty()
                .WithMessage("Debe digitar el número de cédula")
                .MinimumLength(8)
                .MaximumLength(12)
                .WithMessage("La cédula debe de contener entre 8 y 12 caracteres");

            RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage("Debe digitar un nombre")
                .MaximumLength(20)
                .WithMessage("El nombre debe de tener como máximo 20 caracteres");

            RuleFor(p => p.FirstLastName)
                .NotEmpty()
                .WithMessage("Debe digitar al menos el primer apellido")
                .MaximumLength(20)
                .WithMessage("El primer apellido debe de tener como máximo 20 caracteres");

            RuleFor(p => p.SecondLastName)
                .MaximumLength(20)
                .WithMessage("El segundo apellido debe de tener como máximo 20 caracteres");


        }
    }
}
