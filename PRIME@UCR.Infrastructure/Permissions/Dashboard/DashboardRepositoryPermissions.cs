﻿using PRIME_UCR.Domain.Attributes;
using PRIME_UCR.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Infrastructure.Permissions.Dashboard
{
    public abstract class DashboardRepositoryPermissions
    {
        [RequirePermissions(new[] { AuthorizationPermissions.CanSeeIncidentsInfoOnDashboard })]
        public abstract Task<int> GetIncidentsCounterAsync(string modality);
    }
}
