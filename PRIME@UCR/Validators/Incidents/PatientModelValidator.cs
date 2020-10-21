using FluentValidation;
using PRIME_UCR.Application.Dtos.Incidents;

namespace PRIME_UCR.Validators.Incidents
{
    public class PatientModelValidator : AbstractValidator<PatientModel>
    {
        public PatientModelValidator()
        {
            RuleFor(i => i.CedPaciente)
                .NotEmpty()
                .WithMessage("Debe escribir una cédula.");

            RuleFor(i => i.CedPaciente)
                .MaximumLength(9)
                .WithMessage("Debe seleccionar una cédula válida.");

        }
    }
}