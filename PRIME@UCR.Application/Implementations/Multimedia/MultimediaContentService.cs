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

            await repository.InsertAsync(mcontent);
        }

        public MultimediaContent FillMultimediaContent(string patch) {
            MultimediaContent multimedia_content = new MultimediaContent();
            return multimedia_content; 
        }

        public async Task<MultimediaContent> GetByID(int id)
        {
            return await repository.GetByKeyAsync(id);
        }
        public async Task<List<MultimediaContent>> GetActionMultimediaContent(int citaId, int accionId)
        {
            return (await repository.GetByConditionAsync(mc => mc.CitaAccionId == citaId && mc.AccionId == accionId)).ToList();
        }
    }
}