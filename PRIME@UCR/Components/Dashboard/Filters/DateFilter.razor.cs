﻿using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PRIME_UCR.Application.DTOs.Dashboard;

namespace PRIME_UCR.Components.Dashboard.Filters
{
    public partial class DateFilter
    {
        [Parameter] public FilterModel Value { get; set; }
        [Parameter] public EventCallback<FilterModel> ValueChanged { get; set; }
        [Parameter] public EventCallback OnDiscard { get; set; }

        private bool _changesMade = false;
        private async Task OnPickedInitialDate(DateTime? date)
        {
            Value._selectedInitialDate = date;
            if (date == Value.InitialDateFilter)
            {
                _changesMade = false;
            }
            else
            {
                _changesMade = true;
            }
            await ValueChanged.InvokeAsync(Value);
        }
        private async Task OnPickedFinalDate(DateTime? date)
        {
            Value._selectedFinalDate = date;
            if (date == Value.FinalDateFilter)
            {
                /*              Value.ButtonEnabled = true;*/
                _changesMade = false;
            }
        
            else
            {
                _changesMade = true;
            }
            await ValueChanged.InvokeAsync(Value);
        }

        private async Task Discard()
        {
            _changesMade = false;
            Value._selectedInitialDate = Value.InitialDateFilter;
            Value._selectedFinalDate = Value.FinalDateFilter;
            await ValueChanged.InvokeAsync(Value);
        }
        private async Task Save()
        {
            StateHasChanged();
            Value.InitialDateFilter = Value._selectedInitialDate;
            Value.FinalDateFilter = Value._selectedFinalDate;
            if (Value.InitialDateFilter != null|| Value.FinalDateFilter != null)
            {
                Value.ButtonEnabled = true;
            }
            else
            {
                Value.ButtonEnabled = false;
            }
            _changesMade = false;
            await ValueChanged.InvokeAsync(Value);
        }
    }

}
