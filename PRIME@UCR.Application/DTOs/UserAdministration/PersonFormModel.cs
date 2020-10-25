using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Application.DTOs.UserAdministration
{
    public class PersonFormModel
    {

        public PersonFormModel()
        {
            PhoneNumbers = new List<NúmeroTeléfono>();
        }

        public string IdCardNumber { get; set; }

        public string Name { get; set; }

        public string FirstLastName { get; set; }

        public string? SecondLastName { get; set; }

        public char? Sex { get; set; }

        public DateTime? BirthDate { get; set; }

        public List<NúmeroTeléfono>? PhoneNumbers { get; set; }
    }
}
