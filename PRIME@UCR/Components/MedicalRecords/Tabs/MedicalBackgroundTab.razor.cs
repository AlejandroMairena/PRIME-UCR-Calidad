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
        [Parameter] public List<ListaAlergia> ListaAlergia { get; set; }
        private EditContext _contAnte;
        private EditContext _contAle;

        public ListaAntecedentes antecedetePrueba;
        public ListaAlergia AlergiaPrueba;

        protected override async Task OnInitializedAsync()
        {
            _contAnte = new EditContext(ListaAntecedentes);
            _contAle = new EditContext(ListaAlergia);
        }
    }
}
