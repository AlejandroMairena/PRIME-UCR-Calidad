using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using PRIME_UCR.Domain.Models.MedicalRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Components.MedicalRecords.Tabs
{
    public partial class MedicalBackgroundTab
    {
        [Parameter] public List<Antecedentes> Antecedentes { get; set; }

        [Parameter] public List<ListaAntecedentes> ListaAntecedentes { get; set; }
        [Parameter] public List<Alergias> Alergias { get; set; }

        private EditContext _cont;

        List<string> prueba = new List<string>() { "Uws", "Uwus", "Uwo", "Uwu" };

        public ListaAntecedentes antecedetePrueba;

        protected override async Task OnInitializedAsync()
        {
            _cont = new EditContext(ListaAntecedentes);
        }
    }
}
