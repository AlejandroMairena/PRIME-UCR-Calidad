﻿using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.Services.CheckLists;
using PRIME_UCR.Domain.Models.CheckLists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Components.CheckLists
{
    public partial class CheckListMenuIncidentBase : ComponentBase
    {
       public IEnumerable<InstanceChecklist> instancelists { get; set; }

        public IEnumerable<CheckList> lists { get; set; }
        [Inject] protected ICheckListService MyCheckListService { get; set; }
        [Inject] protected IInstanceChecklistService MyCheckInstanceChechistService { get; set; }
        public CheckList list { get; set; }
        [Parameter] public string insidentcod { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await RefreshModels();
        }
        protected async Task RefreshModels()
        {
            lists = await MyCheckListService.GetAll();// to change
            instancelists = await MyCheckInstanceChechistService.GetByIncidentCod(insidentcod);
        }
        public string GetName2(int id)
        {
            GetName(id);
            return list.Nombre;
        }
        public async Task GetName(int id)
        {

            list = await MyCheckListService.GetById(id);

        }
    }
}
