using PRIME_UCR.Components.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.Dtos.Incidents;
using PRIME_UCR.Application.Services.Appointments;
using PRIME_UCR.Application.Services.Multimedia;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Domain.Models.Appointments;

namespace PRIME_UCR.Components.Incidents.IncidentDetails.Tabs
{
    public partial class MultimediaTab
    {
        [Inject] public IAppointmentService AppointmentService { get; set; }
        [Inject] public IMultimediaContentService MultimediaContentService { get; set; }
        [Parameter] public IncidentDetailsModel Incident { get; set; }
        private List<TipoAccion> _actionTypes;
        private TipoAccion _selectedActionType;
        private List<MultimediaContent> _existingFiles;
        private bool _isLoading = true;
        private string Message = "";

        protected override async Task OnInitializedAsync()
        {
            _actionTypes =
                (await AppointmentService.GetActionTypesAsync())
                .ToList();
            _isLoading = false;
        }

        private async Task OnActionTypeChange(TipoAccion actionType)
        {
            _isLoading = true;
            StateHasChanged();
            _selectedActionType = actionType;
            _existingFiles =
                (await MultimediaContentService.GetByAppointmentAction(Incident.AppointmentId, _selectedActionType.Nombre))
                    .ToList();
            _isLoading = false;
        }

        private async Task OnFileUpload(MultimediaContent mc)
        {
            _isLoading = true;
            StateHasChanged();
            await MultimediaContentService.AddMultContToAction(Incident.AppointmentId, _selectedActionType.Nombre, mc.Id);
            _existingFiles = 
                (await MultimediaContentService.GetByAppointmentAction(Incident.AppointmentId, _selectedActionType.Nombre))
                    .ToList();
            _isLoading = false;
        }
    }
}
