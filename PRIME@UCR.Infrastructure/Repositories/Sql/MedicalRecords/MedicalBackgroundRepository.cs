﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Application.Repositories.MedicalRecords;
using PRIME_UCR.Domain.Models.MedicalRecords;
using PRIME_UCR.Infrastructure.DataProviders;

namespace PRIME_UCR.Infrastructure.Repositories.Sql.MedicalRecords
{
    public class MedicalBackgroundRepository : GenericRepository<Antecedentes, int>, IMedicalBackgroundRepository
    {
        public MedicalBackgroundRepository(ISqlDataProvider dataProvider) : base(dataProvider)
        {
        }
        public override async Task<IEnumerable<Antecedentes>> GetByConditionAsync(Expression<Func<Antecedentes, bool>> expression)
        {
            return await _db.MedicalBackground
                .Include(e => e.Expediente)
                .Include(e => e.ListaAntecedentes).Where(expression).ToListAsync();
        }
    }
}