﻿using PRIME_UCR.Domain.Models.Appointments;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Repositories.Appointments
{
    public interface IHavePrescriptionRepository : IGenericRepository<PoseeReceta, int>
    {
        Task<IEnumerable<PoseeReceta>> GetPrescriptionByAppointmentId(int id);

    }
}
