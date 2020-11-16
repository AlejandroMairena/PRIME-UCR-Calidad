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
        [Parameter] public List<PadecimientosCronicos> PadecimientosCronicos { get; set; }
        [Parameter] public List<ListaPadecimiento> ListaPadecimiento { get; set; }
        [Parameter] public int idExpediente { get; set; }
        private EditContext _contAnte;
        private EditContext _contAle;
        private EditContext _contCond;

        public ListaAntecedentes antecedentePrueba;
        public ListaAlergia AlergiaPrueba;
        public ListaPadecimiento PadecimientoPrueba;

        private bool backgroundAlreadyAdded; 
        private bool showBackground;
        private bool allergyAlreadyAdded;
        private bool showAllergy;
        private bool ChronicConditionAlreadyAdded;
        private bool showChronicCondition;

        MatTheme AddButtonTheme = new MatTheme()
        {
            Primary = "white",
            Secondary = "#095290"
        };

        protected override async Task OnInitializedAsync()
        {
            _contAnte = new EditContext(ListaAntecedentes);
            _contAle = new EditContext(ListaAlergia);
            _contCond = new EditContext(ListaPadecimiento);
            showBackground = false;
            showAllergy = false;
            backgroundAlreadyAdded = false;
            allergyAlreadyAdded = false;
            showChronicCondition = false;
            ChronicConditionAlreadyAdded = false;
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

        private void showChronicConditionOptions()
        {
            if (showChronicCondition)
            {
                showChronicCondition = false;
            }
            else
            {
                showChronicCondition = true;
            }
        }

        private bool ifExistsChronicCondition(int id)
        {
            bool result = false;
            for (int i = 0; i < PadecimientosCronicos.Count && !result; ++i)
            {
                if (PadecimientosCronicos[i].IdListaPadecimiento == id)
                {
                    result = true;
                }
            }
            return result;
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
                        IdExpediente = idExpediente,
                        FechaCreacion = DateTime.Now
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
                        IdExpediente = idExpediente,
                        FechaCreacion = DateTime.Now
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

        private async Task insertChronicCondition()
        {
            if (PadecimientoPrueba != null)
            {
                if (!ifExistsChronicCondition(PadecimientoPrueba.Id))
                {
                    PadecimientosCronicos ChronicCondition = new PadecimientosCronicos()
                    {
                        IdListaPadecimiento = PadecimientoPrueba.Id,
                        IdExpediente = idExpediente
                    };
                    showChronicCondition = false;
                    ChronicConditionAlreadyAdded = false;
                    await ChronicConditionService.InsertChronicConditionAsync(ChronicCondition);
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
