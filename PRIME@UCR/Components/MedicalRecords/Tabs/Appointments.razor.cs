using PRIME_UCR.Domain.Models.MedicalRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;
using PRIME_UCR.Application.Services.MedicalRecords;
using PRIME_UCR.Application.Services.Incidents; 
using PRIME_UCR.Application.DTOs.MedicalRecords;
using PRIME_UCR.Domain.Models;
using BlazorTable;

namespace PRIME_UCR.Components.MedicalRecords.Tabs
{
    public partial class Appointments
    {
        [Parameter] public Expediente medical_record {get; set;}

        public List<Cita> appointments { get; set; }

        public List<Incidente> incidents { get; set; }

        public List<DateIncidentModel> dat_in_link { get; set; }

        //public List<bool> is_it_incident { get; set; }

        public ITable<Cita> AppointmentsModel { get; set;  }

        public bool are_there_appointments { get; set; } = false; 

        protected override async Task OnParametersSetAsync()
        {

            if (medical_record != null && medical_record.Citas != null)
            {
                appointments = medical_record.Citas;
                are_there_appointments = true; 
            }
            else {
                if (medical_record == null)
                {
                    //no esta llegando nunca registro
                }
                else { 
                    //el registro no posee citas. 
                }
            }

            await getIncidents(); 

        }


        public async Task getIncidents() {
            dat_in_link = new List<DateIncidentModel>(); 

            for (int index = 0; index < appointments.Count; ++index) {

                Incidente incident = await incident_service.GetIncidentByDateCodeAsync(appointments[index].Id);
                if (incident != null)
                {
                    DateIncidentModel d_i = new DateIncidentModel()
                    {
                        date = appointments[index],
                        incident = incident
                    };

                    dat_in_link.Add(d_i);
                }
                else { 
                    //es una cita médica y no un incidente. 
                }

            }
        }

    }
}
