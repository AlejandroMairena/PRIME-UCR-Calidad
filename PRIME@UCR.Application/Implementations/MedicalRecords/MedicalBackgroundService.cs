﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using PRIME_UCR.Application.DTOs.MedicalRecords;
using PRIME_UCR.Application.Repositories.Incidents;
using PRIME_UCR.Application.Repositories.MedicalRecords;
using PRIME_UCR.Application.Repositories.UserAdministration;
using PRIME_UCR.Application.Services.MedicalRecords;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Domain.Models.MedicalRecords;
using PRIME_UCR.Domain.Models.UserAdministration;
using PRIME_UCR.Infrastructure.Repositories.Sql.MedicalRecords;

namespace PRIME_UCR.Application.Implementations.MedicalRecords
{
    public class MedicalBackgroundService : IMedicalBackgroundService
    {
        private readonly IMedicalBackgroundRepository _repo;
        private readonly IMedicalBackgroundListRepository _repoLista;
        public MedicalBackgroundService(IMedicalBackgroundRepository repo, IMedicalBackgroundListRepository repoLista)
        {
            _repo = repo;
            _repoLista = repoLista;
        }

        public async Task<IEnumerable<Antecedentes>> GetBackgroundByRecordId(int recordId)
        {
            return await _repo.GetByConditionAsync(i => i.IdExpediente == recordId);
        }
        public async Task<IEnumerable<ListaAntecedentes>> GetAll()
        {
            return await _repoLista.GetAllAsync();
        }

        public async Task InsertBackgroundAsync(int recordId, List<ListaAntecedentes> model)
        {
            await _repo.ClearMedicalBackground(recordId);
            if (model.Count > 0)
            {
                
                foreach (ListaAntecedentes background in model)
                {
                    Antecedentes _bg = new Antecedentes()
                    {
                        IdExpediente = recordId,
                        IdListaAntecedentes = background.Id,
                        FechaCreacion = DateTime.Now

                    };
                    await _repo.InsertAsync(_bg);
                }

            }
        }

        

    }
}