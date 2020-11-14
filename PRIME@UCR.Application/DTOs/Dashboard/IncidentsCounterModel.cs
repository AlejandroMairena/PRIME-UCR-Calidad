using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Application.DTOs.Dashboard
{
    public class IncidentsCounterModel
    {
        public IncidentsCounterModel()
        {
            assignedIncidentsCounter = 0;
        }

        public int totalIncidentsCounter;

        public int groundIncidentsCounter;

        public int airIncidentsCounter;

        public int maritimeIncidents;

        public int assignedIncidentsCounter;
    }
}
