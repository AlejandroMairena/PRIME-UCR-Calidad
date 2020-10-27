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
        private List<List<MultimediaContent>> _existingFiles;
        private bool _isLoading = true;

        protected override async Task OnInitializedAsync()
        {
            _actionTypes =
                (await AppointmentService.GetActionTypesAsync())
                .ToList();
            _existingFiles = new List<List<MultimediaContent>>();
            foreach (var i in _actionTypes)
            {
                var content =
                    (await MultimediaContentService.GetByAppointmentAction(Incident.AppointmentId, i.Nombre))
                    .ToList();
                _existingFiles.Add(content);
            }
            _isLoading = false;
        }

        private async Task OnFileUpload(TipoAccion action, MultimediaContent mc)
        {
            _isLoading = true;
            StateHasChanged();

            var i = _actionTypes.IndexOf(action);
            await MultimediaContentService.AddMultContToAction(Incident.AppointmentId, action.Nombre, mc.Id);
            _existingFiles[i].Add(mc);
            
            _isLoading = false;
        }
    }
}
