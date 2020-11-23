using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PRIME_UCR.Pages.Records;
using PRIME_UCR.Domain.Models.Appointments;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;

namespace PRIME_UCR.Pages.Appointments
{
    public partial class DrugSelectorComponent
    {
        private EditContext _context;

        public bool searching_drug { get; set; } = false;
        public DrugFilter d_filter { get; set; }

        [Parameter] public string IdMedicalPrescription { get; set; }

        [Parameter] public EventCallback<bool> UpdateList { get; set; }

        public int start_show_record { get; set; } = 0;

        public int end_show_record { get; set; }

        public int amount_records { get; set; } = 3;

        public int radius { get; set; } = 3;

        public int total_pages { get; set; } = 3;

        public bool add_prescription_selected { get; set; } = false;

        public int current_page { get; set; } = 1;


        public int total_elements { get; set; } = 0;

        public int sub_group_range { get; set; }


        public List<RecetaMedica> prescriptions { get; set; }

        public bool display_existed_msg { get; set; } = false; 

        public async Task add_prescription(RecetaMedica drug_)
        {
            PoseeReceta pr = await appointment_service.GetDrugByConditionAsync(drug_.Id);
            if (pr == null)
            {
                await appointment_service.InsertPrescription(drug_.Id, Convert.ToInt32(IdMedicalPrescription));
                await UpdateList.InvokeAsync(true);
                display_existed_msg = false; 
            }
            else {
                display_existed_msg = true; 
            }
            add_prescription_selected = false;
        }

        /*
        public async Task add_prescription(int drug_)
        {
            await appointment_service.InsertPrescription(drug_, Convert.ToInt32(IdMedicalPrescription));
            add_prescription_selected = false;
        }
        */

        private async Task SetDrugName(string drug_name)
        {
            await set_filter(drug_name);
        }


        private async Task set_filter(string filter_name)
        {
            searching_drug = true;
            current_page = 1;
            await get_records_with_filter(filter_name);
            searching_drug = false;
        }

        private async Task clear_filter()
        {
            await get_records();
        }



        public void ShowDrugSelector()
        {
            add_prescription_selected = true;
        }


        private async Task set_records_per_page(int amount_records)
        {

            start_values(amount_records);
        }

        async Task get_records_with_filter(string filter_box)
        {

            prescriptions = (await appointment_service.GetDrugsByConditionAsync(filter_box)).ToList();

            current_page = 1;
            sub_group_range = 1;
            total_elements = prescriptions.Count;
            total_pages = total_elements / sub_group_range;

            int residuals = total_elements % sub_group_range;
            if (residuals != 0)
            {
                ++total_pages;
            }

            if (total_elements > 0)
            {
                end_show_record = start_show_record + sub_group_range;

                if (end_show_record > prescriptions.Count)
                {
                    end_show_record = prescriptions.Count;
                }
            }
            else
            {
                end_show_record = 0;
            }
        }



        private async Task selected_page(int page)
        {
            current_page = page;
            await get_records_per_page(page, total_pages);
        }

        protected override void OnInitialized()
        {

        }

        protected override async Task OnInitializedAsync()
        {
            d_filter = new DrugFilter();
            _context = new EditContext(d_filter);
            await get_records();
        }

        private async Task get_records()
        {

            prescriptions = (await appointment_service.GetDrugsAsync()).ToList();
            start_values(3);

        }



        private void start_values(int sub_group_range)
        {
            current_page = 1;
            start_show_record = 0;

            total_elements = prescriptions.Count;

            this.sub_group_range = sub_group_range;

            total_pages = total_elements / sub_group_range;

            int residuals = total_elements % sub_group_range;
            if (residuals != 0)
            {
                ++total_pages;
            }

            end_show_record = start_show_record + sub_group_range;

            if (total_elements < radius)
            {
                radius = 0;
            }

        }


        async Task get_records_per_page(int current_page, int total_pages)
        {

            start_show_record = sub_group_range * (current_page - 1);

            end_show_record = start_show_record + sub_group_range;

            if (end_show_record > prescriptions.Count)
            {

                end_show_record = prescriptions.Count;

            }

        }


    }
}
