﻿using System.ComponentModel.DataAnnotations;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Application.Dtos.Incidents
{
    public class DestinationModel
    {
        [Required]
        public Ubicacion Destination { get; set; }
    }
}