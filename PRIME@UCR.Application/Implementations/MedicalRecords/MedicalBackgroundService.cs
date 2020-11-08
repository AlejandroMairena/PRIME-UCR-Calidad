﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using PRIME_UCR.Application.DTOs.MedicalRecords;
using PRIME_UCR.Application.Repositories.Incidents;
using PRIME_UCR.Application.Repositories.MedicalRecords;
using PRIME_UCR.Application.Repositories.UserAdministration;
using PRIME_UCR.Application.Services.MedicalRecords;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Domain.Models.MedicalRecords;
using PRIME_UCR.Domain.Models.UserAdministration;
using PRIME_UCR.Infrastructure.Repositories.Sql.MedicalRecords;

namespace PRIME_UCR.Application.Implementations.MedicalRecords
{
    public class MedicalBackgroundService : IMedicalBackgroundService
    {
        private readonly IMedicalBackgroundRepository _repo;

        public MedicalBackgroundService(IMedicalBackgroundRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Antecedentes>> GetBackgroundByRecordId(int recordId)
        {
            IEnumerable<Antecedentes> test = await _repo.GetByConditionAsync(i => i.IdExpediente == recordId);
            return test;
        }
    }
}