using System;
using FluentValidation;
using PRIME_UCR.Domain.Models.UserAdministration;

namespace PRIME_UCR.Validators.Incidents
{
    public class PacienteValidator : AbstractValidator<Paciente>
    {
        public PacienteValidator()
        {
            RuleFor(p => p.Cédula)
                .NotEmpty()
                .WithMessage("Debe digitar una cédula.");

            RuleFor(p => p.Nombre)
                .NotEmpty()
                .WithMessage("Debe digitar un nombre.");

            RuleFor(p => p.PrimerApellido)
                .NotEmpty()
                .WithMessage("Debe digitar un primer apellido");

            RuleFor(p => p.FechaNacimiento)
                .Must(f => !f.HasValue || DateTime.Now.AddYears(-120).CompareTo(f.Value) <= 0)
                .WithMessage("No puede exceder los 120 años.");
        }
    }
}