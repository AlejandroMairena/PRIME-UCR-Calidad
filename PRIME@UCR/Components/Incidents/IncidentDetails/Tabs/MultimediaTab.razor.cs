using PRIME_UCR.Components.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Components.Incidents.IncidentDetails.Tabs
{
    public partial class MultimediaTab
    {
        [Inject] IAppointmentService AppointmentService { get; set; }
        private List<TipoAccion> _actionTypes;
        private TipoAccion _selectedActionType;

        protected override async Task OnInitializedAsync()
        {
            _actionTypes = await AppointmentService.GetActionTypesAsync();
        }
    }
}
