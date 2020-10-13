using PRIME_UCR.Application.Services;
using PRIME_UCR.Application.Repositories;
using PRIME_UCR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PRIME_UCR.Application.Repositories.Multimedia;
using PRIME_UCR.Application.Services.Multimedia;
using System.Linq;

namespace PRIME_UCR.Application.Implementations.Multimedia
{
    public class MultimediaContentService : IMultimediaContentService
    {
        //public IFileListEntry file;
        private readonly IMultimediaContentRepository repository; 

        public MultimediaContentService(IMultimediaContentRepository repository ) {
            this.repository = repository;
            
        }

        public async Task AddFileAsync(MultimediaContent mcontent) {

            //abrir aqui el archivo que se desea adjuntar. 
            //string path = "c:/ Temp / MM / "; //esto es un ejemplo
            await repository.InsertAsync(mcontent);

            //return FillMultimediaContent(path); 
        }

        public MultimediaContent FillMultimediaContent(string patch) {

            //aqui se llenarian los datos antes de guardarlo a la db. 

            MultimediaContent multimedia_content = new MultimediaContent();
            return multimedia_content; 
        }

        public async Task<MultimediaContent> GetByID(int id)
        {
            return await repository.GetByKeyAsync(id);
        }

        public async Task<List<MultimediaContent>> GetByActionID(int actionID)
        {
            return (await repository.GetByConditionAsync(mc => mc.ID_accion == actionID)).ToList();
        }

    }
}