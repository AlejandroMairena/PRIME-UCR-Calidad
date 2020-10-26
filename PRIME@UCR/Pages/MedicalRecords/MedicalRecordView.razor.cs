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

namespace PRIME_UCR.Pages.MedicalRecords
{
    public partial class MedicalRecordView
    {

        [Parameter]
        public int Id { get; set; }

        private readonly List<Tuple<DetailsTab, string>> _tabs = new List<Tuple<DetailsTab, string>>();

        const DetailsTab DefaultTab = DetailsTab.Info;

        private DetailsTab _activeTab = DefaultTab;

        protected bool exists = true;

        private Expediente record;

        private Persona person;

        private RecordViewModel viewModel;

        protected override async Task OnInitializedAsync()
        {
            viewModel = await MedicalRecordService.GetIncidentDetailsAsync(Id);
            if (record == null)
                exists = false;
        }



    }
}
