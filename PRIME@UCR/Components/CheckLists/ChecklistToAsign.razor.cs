﻿using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.Services;
using PRIME_UCR.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using PRIME_UCR.Application.Services.CheckLists;
using PRIME_UCR.Components.CheckLists;
using Microsoft.AspNetCore.Components.Forms;
using System.Linq;
using PRIME_UCR.Domain.Models.CheckLists;

namespace PRIME_UCR.Components.CheckLists
{
    public class ChecklistToAsignBase : ComponentBase
    {

        public ChecklistToAsignBase()
        {
            llenado = false;
            count = 0;
            dont_save = true;
        }
        protected IEnumerable<CheckList> lists { get; set; }
        public bool llenado;
        [Inject] protected ICheckListService MyService { get; set; }
        public List<CheckList> TempInstance = new List<CheckList>();
        public List<Todo> TempDetail = new List<Todo>();
        public List<int> TempsIds = new List<int>();
        public int count;
        public bool dont_save;

        protected override async Task OnInitializedAsync()
        {
            await RefreshModels();
        }

        protected async Task RefreshModels()
        {
            lists = await MyService.GetAll();
        }
        protected async Task Update()
        {
            await MyService.UpdateCheckList((CheckList)lists);
            await RefreshModels();
        }
        public void Dispose()
        {
            foreach( var temp in TempDetail )
            {
                temp.IsDone = false;
            }
            Update();
            OnInitialized();
            dont_save = true;

        }
        protected void CheckIempList(int idd, ChangeEventArgs e)
        {
            if ((bool)e.Value)
            {
                TempDetail[TempsIds.IndexOf(idd)].IsDone = true;
                count++;
                update_save();
            }
            else
            {
                TempDetail[TempsIds.IndexOf(idd)].IsDone = false;
                count--;
                update_save();
            }
        }

        public void update_save()
        {
            if (count > 0)
            {
                dont_save = false;
            }
            else
            {
                dont_save = true;
            }
        }



        protected void toggleItemChangeComponent()
        {
        }
        public class Todo
        {
            public bool IsDone { get; set; }
            public int idd;
        }

        public void Llenar()
        {
            if (llenado == false)
            {
                foreach (var templist in lists)
                {
                    Todo TodoItem = new Todo();
                    int idds = @templist.Id;
                    TodoItem.idd = idds;
                    TempDetail.Add(TodoItem);
                    TempsIds.Add(idds);
                }
                llenado = true;
            }
        }
    }
}