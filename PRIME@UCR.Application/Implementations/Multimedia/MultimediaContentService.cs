using PRIME_UCR.Application.Services;
using PRIME_UCR.Application.Repositories;
using PRIME_UCR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
//using BlazorInputFile;


namespace PRIME_UCR.Application.Services.Multimedia
{
    public class MultimediaContentService : IMultimediaContentService
    {
        //public IFileListEntry file;

        public MultimediaContentService() { 
        
        
        }

        public bool AddFile(MultimediaContent mcontent) {

            //abrir aqui el archivo que se desea adjuntar. 
            string path = "c:/ Temp / MM / "; //esto es un ejemplo

            //return FillMultimediaContent(path); 
            return true;
        }

        public MultimediaContent FillMultimediaContent(string patch) {

            //aqui se llenarian los datos antes de guardarlo a la db. 

            MultimediaContent multimedia_content = new MultimediaContent();
            return multimedia_content; 
        }


        /*    
        public async Task UploadAsync(IFileListEntry file) { 
        
        }
        */      
    }
}