using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.Services.Incidents;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;

namespace PRIME_UCR.Components.Incidents.LocationPickers
{
    public partial class MedicalCenterPicker
    {
        [Inject]
        public ILocationService LocationService { get; set; }
        
        [Parameter]
        public Ubicacion Value { get; set; }

        [Parameter]
        public string LocationContext { get; set; } 
        
        [Parameter]
        public EventCallback<Ubicacion> ValueChanged { get; set; }

        [Inject]
        public IPersonService personService { get; set; }

        private CentroMedico _selectedMedicalCenter;

        private Persona _selectedDoctor;

        private string doctorForLabel;

        private int _bedNumber;

        private List<CentroMedico> _values;

        private List<Persona> _doctors;

        private List<TrabajaEn> _doctorsOfMedicalCenters;


        async Task Callback()
        {
            var location = new CentroUbicacion
            {
                CentroMedicoId = _selectedMedicalCenter.Id,
                NumeroCama = _bedNumber,
                CedulaMedico = _selectedDoctor.Cédula
            };
            await ValueChanged.InvokeAsync(location);
        }

        async Task<IEnumerable<Persona>> getDoctorsNames()
        {
            Persona person;
            List<Persona> _doctorsTmp = new List<Persona>();
            foreach (TrabajaEn doc in _doctorsOfMedicalCenters)
            {
                person = await personService.getPersonByIdAsync(doc.CédulaMédico);
                _doctorsTmp.Add(person);
            }
             return _doctorsTmp;
        }

        async Task OnChangeMedicalCenter(CentroMedico medicalCenter)
        {
            _selectedMedicalCenter = medicalCenter;
            _doctorsOfMedicalCenters = (await LocationService.GetAllDoctorsbyMedicalCenter(_selectedMedicalCenter.Id))
            .ToList();
            _doctors = (await getDoctorsNames()).ToList();
            await Callback();
        }
        async Task OnChangeDoctor(Persona doctor)
        {
            _selectedDoctor = doctor;
            await Callback();
        }

        protected override async Task OnInitializedAsync()
        {
            _values =
                (await LocationService.GetAllMedicalCentersAsync())
                .ToList();

            _doctorsOfMedicalCenters = (await LocationService.GetAllDoctorsbyMedicalCenter(_values.First().Id))
            .ToList();
            _doctors = (await getDoctorsNames()).ToList();
            doctorForLabel = "Médico de " + LocationContext;

            if (Value is CentroUbicacion location)
            {
                _selectedMedicalCenter = _values.First(mc => mc.Id == location.CentroMedicoId);
                _bedNumber = location.NumeroCama;
                _selectedDoctor = _doctors.First();
            }
            else
            {
                _selectedMedicalCenter = _values.First();
                _selectedDoctor = _doctors.First();
                await Callback();
            }

        }
    }
}