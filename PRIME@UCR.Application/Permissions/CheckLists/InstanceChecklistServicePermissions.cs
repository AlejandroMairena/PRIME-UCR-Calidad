using PRIME_UCR.Domain.Attributes;
using PRIME_UCR.Domain.Constants;
using PRIME_UCR.Domain.Models.UserAdministration;
using PRIME_UCR.Application.DTOs.Incidents;
using PRIME_UCR.Domain.Models.Incidents;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PRIME_UCR.Application.Dtos.Incidents;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Domain.Models.CheckLists;

namespace PRIME_UCR.Application.Permissions.CheckLists
{
    public abstract class InstanceChecklistServiceAuthorization
    {
        [RequirePermissions(new AuthorizationPermissions[] { AuthorizationPermissions.CanInstantiateChecklist })]
        public abstract Task<InstanceChecklist> InsertInstanceChecklist(InstanceChecklist list);

        [RequirePermissions(new AuthorizationPermissions[] { AuthorizationPermissions.CanManageIncidentChecklists })]
        public abstract Task<InstanceChecklist> UpdateInstanceChecklist(InstanceChecklist list);

        [RequirePermissions(new AuthorizationPermissions[] { AuthorizationPermissions.CanManageIncidentChecklists })]
        public abstract Task DeleteInstanceChecklist(int id, string cod);

        [RequirePermissions(new AuthorizationPermissions[] { AuthorizationPermissions.CanInstantiateChecklist })]
        public abstract Task<InstanciaItem> InsertInstanceItem(InstanciaItem instanceItem);

        [RequirePermissions(new AuthorizationPermissions[] { AuthorizationPermissions.CanCheckItemsInChecklists })]
        public abstract Task<InstanciaItem> UpdateItemInstance(InstanciaItem item);
    }
}