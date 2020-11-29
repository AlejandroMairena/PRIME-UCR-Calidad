using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using CsvHelper;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Application.DTOs.Incidents;
using PRIME_UCR.Application.Services.Dashboard;

namespace PRIME_UCR.Application.Implementations.Dashboard
{
    
    public class FileManagerService : IFileManagerService
    {
        public FileManagerService()
        { 
        }

        public string createFile(List<Incidente> filteredIncidentsData)
        {
            List<IncidentListModel> listaIncidentes = new List<IncidentListModel>();
            string PATH = "../PRIME@UCR.Application/FilesToSend/file.csv";
            foreach (Incidente incident in filteredIncidentsData)
            {
                listaIncidentes.Add(new IncidentListModel
                {
                    Codigo = incident.Codigo,
                    Origen = incident.Origen?.GetType().Name,
                    Destino = incident.Destino?.GetType().Name,
                    Modalidad = incident.Modalidad,
                    FechaHoraRegistro = incident.Cita.FechaHoraEstimada,
                    Estado = incident.EstadoIncidentes.ToArray()[incident.EstadoIncidentes.Count -1].NombreEstado,
                    IdDestino = incident.IdDestino
                });
            }
            using (StreamWriter sw = new StreamWriter(PATH, false, new UTF8Encoding(true)))
            {
                using (CsvWriter cw = new CsvWriter(sw))
                { 
                    cw.WriteHeader<IncidentListModel>();
                    cw.NextRecord();
                    foreach (IncidentListModel incidentToAdd in listaIncidentes)
                    {
                        cw.WriteRecord<IncidentListModel>(incidentToAdd);
                        cw.NextRecord();
                    }
                    
                }

            }
            return PATH;
        }

    }

}
