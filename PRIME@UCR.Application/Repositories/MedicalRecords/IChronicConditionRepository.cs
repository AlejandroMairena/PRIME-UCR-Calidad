using System;
using System.Collections.Generic;
using System.Text;
using PRIME_UCR.Domain.Models.MedicalRecords;

namespace PRIME_UCR.Application.Repositories.MedicalRecords
{
    public interface IChronicConditionRepository : IGenericRepository<PadecimientosCronicos, int>
    {
    }
}
