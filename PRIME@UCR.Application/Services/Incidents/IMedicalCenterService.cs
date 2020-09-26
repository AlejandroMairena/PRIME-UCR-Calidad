using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Application.Services.Incidents
{
    public interface IMedicalCenterService
    {
        CentroMedico GetAllAsync();
    }
}