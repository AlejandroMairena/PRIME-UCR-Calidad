using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Domain.Models.MedicalRecords;
using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Components.MedicalRecords
{
    public partial class MedicalRecordForm
    {
        public bool is_loading = false;

        private EditContext _context;

        private EditContext RecordContext;

        private EditContext PatientContext;

        private Paciente patient;

        private Expediente record;

        public int patient_status = -1;

        public int medic_status = -1;

        public bool save_record_data_pushed = false;

        public bool save_patient_data_pushed = false;

        public bool patient_inserted = false;

        public bool record_inserted = false;

        public bool is_loading_record = false;

        public bool is_loading_patient = false;

        public bool record_unfinisehd = false;

        public CentroMedico clinic { get; set; }

        public Gender gender = new Gender();

        public bool not_done = false;

        public List<CentroMedico> clinics = new List<CentroMedico>
{
        //new CentroMedico { Nombre = "Centro Nacional de Rehabilitación Humberto Araya Rojas"},
        new CentroMedico { Nombre = "Hospital México"},
        new CentroMedico { Nombre = "Hospital Cima" },
        new CentroMedico { Nombre = "Hospital CEACO"}

    };

        public List<Gender> genders;

        public CentroMedico medical_center = new CentroMedico();

        [Parameter] public RecordModel RecordModel { get; set; }
        [Parameter] public string Submit_msg { get; set; }
        [Parameter] public EventCallback OnValidSubmit { get; set; }

        private readonly List<Gender?> Genders = new List<Gender?>();

        private async Task SaveData()
        {
            is_loading_record = true;

            medic_status = -1;

            save_record_data_pushed = true;

            if (patient_status != 0)
            {

                if (RecordModel?.CentroMedico.Nombre != null)
                {
                    RecordModel.Expediente.Clinica = RecordModel.CentroMedico.Nombre;
                }

                if (RecordModel.CedMedicoDuenno != null)
                {
                    RecordModel.Expediente.CedulaMedicoDuenno = RecordModel.CedMedicoDuenno;
                }

                if (save_patient_data_pushed || patient_status != 2)
                {
                    await medical_record_service.InsertAsync(RecordModel.Expediente);
                    record_inserted = true;
                    is_loading_record = false;

                    await redirect();
                    RecordModel.set_to_null();
                }
                else
                {
                    record_unfinisehd = true;
                }
            }
            else
            {

                record_inserted = true;
                is_loading_record = false;

                if (RecordModel.CedMedicoDuenno != null)
                {
                    RecordModel.Expediente.CedulaMedicoDuenno = RecordModel.CedMedicoDuenno;
                }
                if (RecordModel?.CentroMedico.Nombre != null)
                {
                    RecordModel.Expediente.Clinica = RecordModel.CentroMedico.Nombre;
                }

                await medical_record_service.UpdateMedicalRecordAsync(RecordModel.Expediente);
                RecordModel.set_to_null();

            }
            is_loading_patient = true;

            save_patient_data_pushed = true;

            if (patient_status == 2)
            {

                RecordModel.Paciente.Sexo = RecordModel.Sexo.ToString();

                if (RecordModel.Paciente.Nombre != null && RecordModel.Paciente.PrimerApellido != null)
                {

                    await patient_service.CreatePatientAsync(RecordModel.Paciente);
                    patient_inserted = true;

                    if (save_record_data_pushed)
                    {
                        await medical_record_service.InsertAsync(RecordModel.Expediente);
                        record_inserted = true;
                        record_unfinisehd = false;
                        await redirect();
                        RecordModel.set_to_null();
                    }
                    is_loading_record = false;
                }
                else
                {
                    not_done = true;
                }

            }
            is_loading_patient = false;

        }

        //private async Task SaveRecordData()
        //{
        //    is_loading_record = true;

        //    medic_status = -1;

        //    save_record_data_pushed = true;

        //    if (patient_status != 0)
        //    {

        //        if (RecordModel?.CentroMedico.Nombre != null)
        //        {
        //            RecordModel.Expediente.Clinica = RecordModel.CentroMedico.Nombre;
        //        }

        //        if (RecordModel.CedMedicoDuenno != null)
        //        {
        //            RecordModel.Expediente.CedulaMedicoDuenno = RecordModel.CedMedicoDuenno;
        //        }

        //        if (save_patient_data_pushed || patient_status != 2)
        //        {
        //            await medical_record_service.InsertAsync(RecordModel.Expediente);
        //            record_inserted = true;
        //            is_loading_record = false;

        //            await redirect();
        //            RecordModel.set_to_null();
        //        }
        //        else
        //        {
        //            record_unfinisehd = true;
        //        }
        //    }
        //    else
        //    {

        //        record_inserted = true;
        //        is_loading_record = false;

        //        if (RecordModel.CedMedicoDuenno != null)
        //        {
        //            RecordModel.Expediente.CedulaMedicoDuenno = RecordModel.CedMedicoDuenno;
        //        }
        //        if (RecordModel?.CentroMedico.Nombre != null)
        //        {
        //            RecordModel.Expediente.Clinica = RecordModel.CentroMedico.Nombre;
        //        }

        //        await medical_record_service.UpdateMedicalRecordAsync(RecordModel.Expediente);
        //        RecordModel.set_to_null();

        //    }

        //}

        //private async Task SavePatientData()
        //{
        //    is_loading_patient = true;

        //    save_patient_data_pushed = true;

        //    if (patient_status == 2)
        //    {

        //        RecordModel.Paciente.Sexo = RecordModel.Sexo.ToString();

        //        if (RecordModel.Paciente.Nombre != null && RecordModel.Paciente.PrimerApellido != null)
        //        {

        //            await patient_service.CreatePatientAsync(RecordModel.Paciente);
        //            patient_inserted = true;

        //            if (save_record_data_pushed)
        //            {
        //                await medical_record_service.InsertAsync(RecordModel.Expediente);
        //                record_inserted = true;
        //                record_unfinisehd = false;
        //                await redirect();
        //                RecordModel.set_to_null();
        //            }
        //            is_loading_record = false;
        //        }
        //        else
        //        {
        //            not_done = true;
        //        }

        //    }
        //    is_loading_patient = false;
        //}


        private async Task redirect()
        {

            Expediente exp = await medical_record_service.GetByPatientIdAsync(RecordModel.Paciente.Cédula);
            if (exp != null)
            {
                string path = "/medicalrecord/";
                path += exp.Id.ToString();
                path += "/created";
                NavManager.NavigateTo($"{path}");
            }
            else
            {
                //something went wrong.
            }

        }

        protected override async Task OnInitializedAsync()
        {
            patient = new Paciente();
            record = new Expediente();

            //Something's not working well here.
            //IEnumerable<CentroMedico> cm = await medical_record_service.GetMedicalCentersAsync();
            //Clinicas = cm.ToList();

            genders = new List<Gender>();
            Gender pivote_gender = new Gender();
            pivote_gender = Gender.Male;
            genders.Add(pivote_gender);
            pivote_gender = Gender.Female;
            genders.Add(pivote_gender);
            pivote_gender = Gender.Unspecified;
            genders.Add(pivote_gender);

            RecordModel.CentroMedico = new CentroMedico();

            RecordContext = new EditContext(record);
            PatientContext = new EditContext(patient);
            _context = new EditContext(RecordModel);
        }


        private async Task SetMedicCed(string ced)
        {

            RecordModel.CedMedicoDuenno = ced;
            record.CedulaMedicoDuenno = ced;

            if (RecordContext.Validate())
            {

                Médico medic = await doctor_service.GetDoctorByIdAsync(ced);
                if (medic != null)
                {
                    medic_status = 2;

                }
                else
                {
                    RecordModel.CedMedicoDuenno = null;
                    medic_status = 1;
                }
            }
            else
            {

            }


        }

        private void MedicalCenterSelected(CentroMedico mc)
        {

        }


        private async Task SetPatientCed(string ced)
        {
            patient_inserted = false;
            record_inserted = false;
            is_loading = true;
            RecordModel.CedPaciente = ced;

            if (_context.Validate())
            {

                Persona person = await person_service.GetPersonByIdAsync(ced);

                if (person != null)
                {
                    //person already exist
                    Paciente patient = await patient_service.GetPatientByIdAsync(ced);

                    if (patient != null)
                    {
                        //patient already exist
                        RecordModel.Paciente = patient;
                        _context = new EditContext(RecordModel);
                    }
                    else
                    {
                        //person exist but not patient
                        RecordModel.Paciente = new Paciente()
                        {
                            Cédula = person.Cédula,
                            Nombre = person.Nombre,
                            PrimerApellido = person.PrimerApellido,
                            SegundoApellido = person.SegundoApellido,
                            FechaNacimiento = person.FechaNacimiento,
                            Sexo = person.Sexo
                        };

                        _context = new EditContext(RecordModel);
                        await patient_service.InsertPatientOnlyAsync(RecordModel.Paciente);
                    }

                    Expediente record = await medical_record_service.GetByPatientIdAsync(ced);
                    if (record != null)
                    {
                        //the medical record already exist.
                        RecordModel.Expediente = record;
                        patient_status = 0;
                    }
                    else
                    {
                        //medical record doesnt exist.
                        RecordModel.Expediente = new Expediente()
                        {
                            CedulaPaciente = ced
                        };

                        //some information might be missing, so, the insert of the record is not automatic.
                        patient_status = 1;
                    }

                }
                else
                {
                    //neither the person or the record exist.
                    RecordModel.Paciente = new Paciente()
                    {
                        Cédula = ced
                    };

                    _context = new EditContext(RecordModel);

                    patient_status = 2;

                    RecordModel.Expediente = new Expediente() { CedulaPaciente = ced };

                }
            }
            is_loading = false;
        }
    }
}
