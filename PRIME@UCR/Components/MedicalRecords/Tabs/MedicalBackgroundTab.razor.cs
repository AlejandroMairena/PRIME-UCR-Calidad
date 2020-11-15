using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PRIME_UCR.Domain.Models.MedicalRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PRIME_UCR.Application.Services.MedicalRecords;
using PRIME_UCR.Application.Implementations.MedicalRecords;

namespace PRIME_UCR.Components.MedicalRecords.Tabs
{
    public partial class MedicalBackgroundTab
    {
        [Parameter] public List<Antecedentes> Antecedentes { get; set; }
        [Parameter] public List<ListaAntecedentes> ListaAntecedentes { get; set; }
        [Parameter] public List<Alergias> Alergias { get; set; }
        [Parameter] public List<ListaAlergia> ListaAlergia { get; set; }
        [Parameter] public int idExpediente { get; set; }
        private EditContext _contAnte;
        private EditContext _contAle;

        public ListaAntecedentes antecedentePrueba;
        public ListaAlergia AlergiaPrueba;

        private bool backgroundAlreadyAdded; 
        private bool showBackground;
        private bool allergyAlreadyAdded;
        private bool showAllergy;

        MatTheme AddButtonTheme = new MatTheme()
        {
            Primary = "white",
            Secondary = "#095290"
        };

        protected override async Task OnInitializedAsync()
        {
            _contAnte = new EditContext(ListaAntecedentes);
            _contAle = new EditContext(ListaAlergia);
            showBackground = false;
            showAllergy = false;
            backgroundAlreadyAdded = false;
            allergyAlreadyAdded = false;
    }

        private void showBackgroundOptions()
        {
            if (showBackground)
            {
                showBackground = false;
            }
            else 
            {
                showBackground = true;
            }
        }

        private bool ifExistsBackground(int id)
        {
            bool result = false;
            for (int i = 0; i < Antecedentes.Count && !result; ++i)
            {
                if (Antecedentes[i].IdListaAntecedentes == id)
                {
                    result = true;
                }
            }
            return result;
        }

        private bool ifExistsAllergy(int id)
        {
            bool result = false;
            for (int i = 0; i < Alergias.Count && !result; ++i)
            {
                if (Alergias[i].IdListaAlergia == id)
                {
                    result = true;
                }
            }
            return result;
        }


        private async Task insertBackground()
        {
            if (antecedentePrueba != null) { 
                if (!ifExistsBackground(antecedentePrueba.Id))
                {
                    Antecedentes background = new Antecedentes()
                    {
                        IdListaAntecedentes = antecedentePrueba.Id,
                        IdExpediente = idExpediente
                    };
                    showBackground = false;
                    backgroundAlreadyAdded = false;
                    await MedicalBackgroundService.InsertBackgroundAsync(background);
                    Antecedentes = (await MedicalBackgroundService.GetBackgroundByRecordId(idExpediente)).ToList();
                    //StateHasChanged();
                }
                else
                {
                    backgroundAlreadyAdded = true;
                }
            }
        }

        private async Task insertAllergy()
        {
            if (AlergiaPrueba != null)
            {
                if (!ifExistsAllergy(AlergiaPrueba.Id))
                {
                    Alergias allergy = new Alergias()
                    {
                        IdListaAlergia = AlergiaPrueba.Id,
                        IdExpediente = idExpediente
                    };
                    showAllergy = false;
                    allergyAlreadyAdded = false;
                    await AllergyService.InsertAllergyAsync(allergy);
                    Alergias = (await AllergyService.GetAlergyByRecordId(idExpediente)).ToList();
                }
                else
                {
                    allergyAlreadyAdded = true;
                }
            }
        }

        private void showAllergyOptions()
        {
            if (showAllergy)
            {
                showAllergy = false;
            }
            else
            {
                showAllergy = true;
            }
        }
    }
}
