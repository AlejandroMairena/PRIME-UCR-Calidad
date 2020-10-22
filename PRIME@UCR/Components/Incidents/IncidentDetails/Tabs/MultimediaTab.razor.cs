using PRIME_UCR.Components.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.Services.Appointments;
using PRIME_UCR.Domain.Models.Appointments;

namespace PRIME_UCR.Components.Incidents.IncidentDetails.Tabs
{
    public partial class MultimediaTab
    {
        [Inject] IAppointmentService AppointmentService { get; set; }
        private List<TipoAccion> _actionTypes;
        private TipoAccion _selectedActionType = new TipoAccion();

        protected override async Task OnInitializedAsync()
        {
            _actionTypes =
                (await AppointmentService.GetActionTypesAsync())
                .ToList();
        }
    }
}
