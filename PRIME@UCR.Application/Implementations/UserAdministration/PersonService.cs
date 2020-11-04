using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using PRIME_UCR.Application.DTOs.UserAdministration;
using PRIME_UCR.Application.Exceptions.UserAdministration;
using PRIME_UCR.Application.Repositories.UserAdministration;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Implementations.UserAdministration
{
    public class PersonService : IPersonService
    {

        private readonly IPersonaRepository PersonRepository;

        private readonly IPrimeSecurityService primeSecurityService;

        public PersonService(IPersonaRepository _personaRepository,
            IPrimeSecurityService _primeSecurityService)
        {
            PersonRepository = _personaRepository;
            primeSecurityService = _primeSecurityService;
        }

        /**
         * Method used to get a person by its id, in this case, by its Cedula
         */
        public async Task<Persona> GetPersonByIdAsync(string id)
        {
            if((await primeSecurityService.isAuthorizedAsync(AuthorizationPolicies.CanManageUsers)))
            {
                return await PersonRepository.GetByKeyPersonaAsync(id);
            } else
            {
                throw new NotAuthorizedException();
            }
        }

        /**
         * Method used to convert a RegisterUserForm to a Person DTO
         * 
         * Return:  A person DTO with its information.
         */
        public async Task<PersonFormModel> GetPersonModelFromRegisterModelAsync(RegisterUserFormModel registerUserModel)
        {
            if ((await primeSecurityService.isAuthorizedAsync(AuthorizationPolicies.CanManageUsers)))
            {
                PersonFormModel personModel = new PersonFormModel();
                personModel.IdCardNumber = registerUserModel.IdCardNumber;
                personModel.Name = registerUserModel.Name;
                personModel.FirstLastName = registerUserModel.FirstLastName;
                personModel.SecondLastName = registerUserModel.SecondLastName;
                personModel.Sex = registerUserModel.Sex.ToString();
                personModel.BirthDate = registerUserModel.BirthDate;
                personModel.PrimaryPhoneNumber = registerUserModel.PrimaryPhoneNumber;
                personModel.SecondaryPhoneNumber = registerUserModel.SecondaryPhoneNumber;
                return personModel;
            } else
            {
                throw new NotAuthorizedException();
            }

        }

        /**
         * Method used to convert a person DTO to the persona model
         * 
         * Return: A person model with the information given by the user
         */
        private Persona GetPersonaFromPersonModel(PersonFormModel personInfo)
        {
            Persona person = new Persona();
            person.Cédula = personInfo.IdCardNumber;
            person.FechaNacimiento = personInfo.BirthDate;
            person.Nombre = personInfo.Name;
            person.PrimerApellido = personInfo.FirstLastName;
            person.SegundoApellido = personInfo.SecondLastName;
            person.Sexo = personInfo.Sex;
            return person;
        }

        /**
         * Method used to store a person in the database
         */
        public async Task StoreNewPersonAsync(PersonFormModel personInfo)
        {
            if ((await primeSecurityService.isAuthorizedAsync(AuthorizationPolicies.CanManageUsers)))
            {
                var person = GetPersonaFromPersonModel(personInfo);
                await PersonRepository.InsertAsync(person);
            } else
            {
                throw new NotAuthorizedException();
            }
        }

        /**
         * Method used to delete a person from the database
         */
        public async Task DeletePersonAsync(string id)
        {
            if ((await primeSecurityService.isAuthorizedAsync(AuthorizationPolicies.CanManageUsers)))
            {
                await PersonRepository.DeleteAsync(id);
            }
            else 
            {
                throw new NotAuthorizedException();
            }
        }
    }
}
