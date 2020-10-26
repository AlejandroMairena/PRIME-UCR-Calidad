using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using PRIME_UCR.Components.MedicalRecords;
using PRIME_UCR.Domain.Models.MedicalRecords;
using PRIME_UCR.Domain.Models.UserAdministration;
using System.Runtime.CompilerServices;
using PRIME_UCR.Application.DTOs.MedicalRecords;
using System.Linq;

namespace PRIME_UCR.Pages.MedicalRecords
{
    public partial class MedicalRecordView
    {

        [Parameter]
        public string Id { get; set; }

        private readonly List<Tuple<DetailsTab, string>> _tabs = new List<Tuple<DetailsTab, string>>();

        const DetailsTab DefaultTab = DetailsTab.Info;

        private DetailsTab _activeTab = DefaultTab;

        protected bool exists = true;

        private Expediente record;

        private Persona person;

        private RecordViewModel viewModel = new RecordViewModel();

        private void FillTabStates()
        {
            _tabs.Clear();
            var tabValues = Enum.GetValues(typeof(DetailsTab)).Cast<DetailsTab>();
            foreach (var tab in tabValues)
            {
                switch (tab)
                {
                    case DetailsTab.Info:
                        _tabs.Add(new Tuple<DetailsTab, string>(DetailsTab.Info, ""));
                        break;
                    case DetailsTab.Appointments:
                        _tabs.Add(new Tuple<DetailsTab, string>(DetailsTab.Appointments, ""));
                        break;
                }
            }
        }


        protected override async Task OnInitializedAsync()
        {
            int identification = Int32.Parse(Id);
            viewModel = await MedicalRecordService.GetIncidentDetailsAsync(identification);
            if (viewModel == null)
                exists = false;
            else
                FillTabStates();
        }



    }
}
