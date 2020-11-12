using PRIME_UCR.Application.DTOs.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Pages.Dashboard
{
    public partial class Dashboard
    {
        public FilterModel FilterInfo = new FilterModel();
        public bool _finishedLoadingCounters = false;
    }
}
