using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using PRIME_UCR.Application.Dtos;
using PRIME_UCR.Application.Dtos.Incidents;
using PRIME_UCR.Application.Repositories.Incidents;
using PRIME_UCR.Application.Services.Incidents;
using PRIME_UCR.Components.Controls;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Pages.Incidents
{
    public partial class CreateIncident : IDisposable
    {
        [Inject]
        public IIncidentService IncidentService { get; set; }
        
        [Inject]
        private NavigationManager NavManager { get; set; }
        
        private IncidentModel _model = new IncidentModel();
        private List<Modalidad> _modes;
        private EditContext _context;
        private bool isFormValid = false;

        private const string DetailsUrl = "/incidents";


        void Redirect(string id)
        {
            NavManager.NavigateTo($"{DetailsUrl}/{id}");
        }
            

        async Task Create()
        {
            var result = await IncidentService.CreateIncident(_model);
            Redirect(result.Codigo);
        }

        protected override async Task OnInitializedAsync()
        {
            _modes =
                (await IncidentService.GetTransportModesAsync())
                .ToList();
            _model.EstimatedDateOfTransfer = DateTime.Now;
            _context = new EditContext(_model);
            _context.OnFieldChanged += HandleFieldChanged;
        }

        private void HandleFieldChanged(object sender, FieldChangedEventArgs e)
        {
            isFormValid = _context.Validate();
            StateHasChanged();
        }

        public void Dispose()
        {
            _context.OnFieldChanged -= HandleFieldChanged;
        }
    }
}