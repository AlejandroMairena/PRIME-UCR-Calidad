﻿using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using PRIME_UCR.Domain.Models.MedicalRecords;
using PRIME_UCR.Domain.Models.UserAdministration;
using PRIME_UCR.Application.DTOs.MedicalRecords;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Application.Services.MedicalRecords
{
    public interface IAlergyService
    {
        Task<IEnumerable<Alergia>> GetAllAsync();

        Task<Alergia> InsertAsync(Alergia alergia);

        Task<Expediente> GetByMedicalRecordIdAsync(int id);
    }
}
