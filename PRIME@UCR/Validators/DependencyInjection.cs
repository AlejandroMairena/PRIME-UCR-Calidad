using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PRIME_UCR.Application.Dtos.Incidents;
using PRIME_UCR.Validators.Incidents;

namespace PRIME_UCR.Validators
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            // incidents
            services.AddTransient<IValidator<IncidentModel>, IncidentModelValidator>();
            services.AddTransient<IValidator<IncidentDetailsModel>, IncidentDetailsModelValidator>();
            services.AddTransient<IValidator<HouseholdModel>, HouseholdModelValidator>();
            services.AddTransient<IValidator<InternationalModel>, InternationalModelValidator>();
            services.AddTransient<IValidator<MedicalCenterLocationModel>, MedicalCenterLocationModelValidator>();
            services.AddTransient<IValidator<PatientModel>, PatientModelValidator>();
            return services;
        }
    }
}