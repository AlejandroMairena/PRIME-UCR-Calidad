using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using CsvHelper;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Application.DTOs.Incidents;
using PRIME_UCR.Application.Services.Dashboard;
using PRIME_UCR.Application.Services.Multimedia;
using PRIME_UCR.Application.Implementations.UserAdministration;
using PRIME_UCR.Application.DTOs.UserAdministration;
using PRIME_UCR.Application.Services.UserAdministration;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Implementations.Dashboard
{
    
    public class FileManagerService : IFileManagerService
    {
        private readonly IFileService fileService;
        private readonly IMailService mailService;

        public FileManagerService(IFileService _fileService, IMailService _mailService)
        {
            fileService = _fileService;
            mailService = _mailService;
        }

        public async Task createFileAsync(List<Incidente> filteredIncidentsData, string userIdentifier)
        {
            List<IncidentListModel> listaIncidentes = new List<IncidentListModel>();
            string PATH = "../PRIME@UCR.Application/FilesToSend/file"+ userIdentifier + ".csv";
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
            
            string container = "";
            string fileName = userIdentifier + ".csv";
            container += "Código,Origen,Destino,Modalidad,Fecha y hora de registro,Estado,Id Destino\n";

            foreach (IncidentListModel incidente in listaIncidentes)
            {
                container += incidente.Codigo + "," + incidente.Origen + "," + incidente.Destino + "," + incidente.Modalidad + "," + incidente.FechaHoraRegistro + "," + incidente.Estado + "," + incidente.IdDestino + "\n";
            }
            string path = await fileService.StoreTextFile(container, fileName);

            var message = new EmailContentModel()
            {
                Destination = userIdentifier,
                Subject = "PRIME@UCR: Solicitud de lista de incidentes",
                Body = $"<p>A continuación, se adjunta un archivo .csv con la lista de incidentes actualizada.</p>",
                AttachmentPath = path
            };

            await mailService.SendEmailAsync(message);
            fileService.DeleteFile(path);
        }

    }

}
