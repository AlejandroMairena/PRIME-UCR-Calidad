using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Internal;
using PRIME_UCR.Application.Dtos.Incidents;
using PRIME_UCR.Application.Services.Incidents;
using PRIME_UCR.Components.Controls;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Components.Incidents.LocationPickers
{
    public partial class HouseholdPicker
    {
        [Inject]
        public ILocationService LocationService { get; set; }

        private List<Provincia> _provinces;
        private List<Canton> _cantons;
        private List<Distrito> _districts;

        protected override bool TryParseValueFromString(string value, out HouseholdModel result, out string validationErrorMessage)
        {
            result = Value;
            validationErrorMessage = "";
            return true;
        }

        // Check if everything has been loaded
        bool IsLoading()
        {
            return _provinces == null
                   || _cantons == null
                   || _districts == null;
        }

        async Task Callback()
        {
            if (Value != null && Value.District != null)
                await ValueChanged.InvokeAsync(Value);
            else
                await ValueChanged.InvokeAsync(null);
            EditContext.NotifyFieldChanged(FieldIdentifier);
        }

        async Task LoadProvinces(bool firstLoad)
        {
            // get options
            _provinces =
                (await LocationService.GetProvincesByCountryNameAsync(Pais.DefaultCountry))
                .ToList();

            if (!firstLoad)
                Value.Province = null;
        }

        async Task OnChangeProvince(Provincia province)
        {
            Value.Province = province;
            await LoadCantons(false);
            await LoadDistricts(false);
        }

        async Task LoadCantons(bool firstLoad)
        {
            if (Value.Province != null)
                _cantons =
                    (await LocationService.GetCantonsByProvinceNameAsync(Value.Province.Nombre))
                    .ToList();
            else
                _cantons = new List<Canton>();

            if (!firstLoad)
                Value.Canton = null;
        }

        async Task OnChangeCanton(Canton canton)
        {
            Value.Canton = canton;
            await LoadDistricts(false);
            await Callback();
        }

        async Task LoadDistricts(bool firstLoad)
        {
            if (Value.Canton != null)
                _districts =
                    (await LocationService.GetDistrictsByCantonIdAsync(Value.Canton.Id))
                    .ToList();
            else
                _districts = new List<Distrito>();

            if (!firstLoad)
                Value.District = null;
        }

        async Task OnChangeDistrict(Distrito district)
        {
            Value.District = district;
            await Callback();
        }

        public async Task LoadExistingValues()
        {
            await LoadProvinces(true);
            await LoadCantons(true);
            await LoadDistricts(true);
        }
        
        protected override async Task OnInitializedAsync()
        {
            if (Value == null)
                throw new ArgumentNullException("Value", "Value argument cannot be null.");
            await LoadExistingValues();
        }
    }
}
