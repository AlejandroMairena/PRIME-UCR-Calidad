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
    public abstract class CheckListServiceAuthorization
    {
        [RequirePermissions(new AuthorizationPermissions[] { AuthorizationPermissions.CanCreateChecklist })]
        public abstract Task<CheckList> InsertCheckList(CheckList list);

        [RequirePermissions(new AuthorizationPermissions[] { AuthorizationPermissions.CanCreateChecklist })]
        public abstract Task<CheckList> UpdateCheckList(CheckList list);

        [RequirePermissions(new AuthorizationPermissions[] { AuthorizationPermissions.CanCreateChecklist })]
        public abstract Task<Item> InsertCheckListItem(Item item);

        [RequirePermissions(new AuthorizationPermissions[] { AuthorizationPermissions.CanCreateChecklist })]
        public abstract Task<Item> SaveImageItem(Item item);

        [RequirePermissions(new AuthorizationPermissions[] { AuthorizationPermissions.CanCreateChecklist })]
        public abstract Task<Item> UpdateItem(Item item);

    }
}
