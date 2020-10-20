using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore.Internal;
using PRIME_UCR.Application.Dtos.Incidents;
using PRIME_UCR.Application.Services.Incidents;
using PRIME_UCR.Components.Controls;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Components.Incidents.LocationPickers
{
    public partial class HouseholdPicker : IDisposable
    {
        [Inject] public ILocationService LocationService { get; set; }
        [Parameter] public HouseholdModel Value { get; set; }
        [Parameter] public EventCallback<HouseholdModel> OnSave { get; set; }
        [Parameter] public EventCallback OnDiscard { get; set; }

        private List<Provincia> _provinces;
        private List<Canton> _cantons;
        private List<Distrito> _districts;
        private bool _saveButtonEnabled = false;
        private EditContext _context;
        private HouseholdModel _initialValue;
        
        // Check if everything has been loaded
        bool IsLoading()
        {
            return _provinces == null
                   || _cantons == null
                   || _districts == null;
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

        void OnChangeDistrict(Distrito district)
        {
            Value.District = district;
        }

        void OnChangeAddress(string address)
        {
            Value.Address = address;
        }

        void OnChangeLongitude(double? newLongitude)
        {
            Value.Longitude = newLongitude;
        }
        
        void OnChangeLatitude(double? newLatitude)
        {
            Value.Latitude = newLatitude;
        }

        public async Task LoadExistingValues()
        {
            await LoadProvinces(true);
            await LoadCantons(true);
            await LoadDistricts(true);
        }

        private async Task Submit()
        {
            await OnSave.InvokeAsync(Value);
        }

        private async Task Discard()
        {
            Value = _initialValue.Clone();
            await LoadExistingValues();
            _context.OnFieldChanged -= HandleFieldChanged;
            _context = new EditContext(Value);
            _context.OnFieldChanged += HandleFieldChanged;
            _saveButtonEnabled = _context.IsModified();
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            _initialValue = Value.Clone();
            if (Value == null)
                throw new ArgumentNullException("Value", "Value argument cannot be null.");
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadExistingValues();
            _context = new EditContext(Value);
            _context.OnFieldChanged += HandleFieldChanged;
        }

        // used to toggle submit button disabled attribute
        private void HandleFieldChanged(object sender, FieldChangedEventArgs e)
        {
            _saveButtonEnabled = _context.IsModified();
            StateHasChanged();
        }

        public void Dispose()
        {
            _context.OnFieldChanged -= HandleFieldChanged;
        }
    }
}
