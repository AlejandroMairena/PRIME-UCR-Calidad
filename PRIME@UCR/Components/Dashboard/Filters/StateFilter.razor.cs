using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.DTOs.Dashboard;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Components.Dashboard.Filters
{
    public partial class StateFilter
    {
        public List<String> _stateTypes;
        [Parameter] public FilterModel Value { get; set; }
        [Parameter] public EventCallback<FilterModel> ValueChanged { get; set; }

        protected override void OnInitialized()
        {
            _stateTypes = new List<string> {"En proceso de creación",
                                            "Creado",
                                            "Rechazado",
                                            "Aceptado",
                                            "Asignado",
                                            "En preparación",
                                            "En ruta a origen",
                                            "Paciente recolectado en origen",
                                            "En traslado",
                                            "Entregado",
                                            "Reactivación",
                                            "Finalizado"};
        }

        private async Task OnStateChange(string state) 
        {
            Value.StateFilter = state;
            await ValueChanged.InvokeAsync(Value);
        }
    }

}