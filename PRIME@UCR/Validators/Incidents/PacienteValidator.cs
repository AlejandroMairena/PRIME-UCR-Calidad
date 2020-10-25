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
        }
    }
}