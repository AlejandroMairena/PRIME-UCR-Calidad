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

        public List<ListaAntecedentes> _backgroundList = new List<ListaAntecedentes>();
        public ListaAntecedentes antecedentePrueba;
        public ListaAlergia AlergiaPrueba;
        public ListaPadecimiento PadecimientoPrueba;

        private bool backgroundAlreadyAdded; 
        private bool _saveBackgroundButtonEnabled;
        private bool allergyAlreadyAdded;
        private bool showAllergy;
        private bool ChronicConditionAlreadyAdded;
        private bool showChronicCondition;

        private IEnumerable<int> RegisteredBackgrounds
        {
            get => _backgroundList.Select(bg => bg.Id);
            set =>
                   _backgroundList =
                        (from ante in ListaAntecedentes
                         join id in value on ante.Id equals id
                         select ante)
                        .ToList();
        }

        MatTheme AddButtonTheme = new MatTheme()
        {
            Primary = "white",
            Secondary = "#095290"
        };

        protected override async Task OnInitializedAsync()
        {
            LoadRecordBackgrounds();

            _contAle = new EditContext(ListaAlergia);
            _contCond = new EditContext(ListaPadecimiento);
            showAllergy = false;
            backgroundAlreadyAdded = false;
            allergyAlreadyAdded = false;
            showChronicCondition = false;
            ChronicConditionAlreadyAdded = false;
        }

        private async Task SaveMedicalBackground()
        {
            StateHasChanged();
            await MedicalBackgroundService.InsertBackgroundAsync(idExpediente, _backgroundList);
            Antecedentes = (await MedicalBackgroundService.GetBackgroundByRecordId(idExpediente)).ToList();
            _contAnte = new EditContext(_backgroundList);
            _saveBackgroundButtonEnabled = false;
            _contAnte.OnFieldChanged += ToggleSaveButton;
        }

        private void ToggleSaveButton(object? sender, FieldChangedEventArgs e)
        {
            _saveBackgroundButtonEnabled = _contAnte.IsModified();
            StateHasChanged();
        }


        private async Task LoadRecordBackgrounds()
        {
            _contAnte = new EditContext(_backgroundList);
            _saveBackgroundButtonEnabled = false;
            _contAnte.OnFieldChanged += ToggleSaveButton;
            foreach (Antecedentes background in Antecedentes)
            {
                _backgroundList.Add(background.ListaAntecedentes);
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


        public void Dispose()
        {
            if (_contAnte != null)
                _contAnte.OnFieldChanged -= ToggleSaveButton;
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
                        IdExpediente = idExpediente,
                        FechaCreacion = DateTime.Now
                    };
                    showChronicCondition = false;
                    ChronicConditionAlreadyAdded = false;
                    await ChronicConditionService.InsertChronicConditionAsync(ChronicCondition);
                    PadecimientosCronicos = (await ChronicConditionService.GetChronicConditionByRecordId(idExpediente)).ToList();
                }
                else
                {
                    ChronicConditionAlreadyAdded = true;
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
