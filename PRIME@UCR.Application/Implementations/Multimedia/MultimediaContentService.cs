﻿using PRIME_UCR.Application.Services;
using PRIME_UCR.Application.Repositories;
using PRIME_UCR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace PRIME_UCR.Application.Services.Multimedia
{
    public class MultimediaContentService : IMultimediaContentService
    {

        public MultimediaContentService() { 
        
        
        }

        public MultimediaContent AddFile() {

            //abrir aqui el archivo que se desea adjuntar. 
            string path = ""; 

            return FillMultimediaContent(path); 

        }

        public MultimediaContent FillMultimediaContent(string patch) {

            //aqui se llenarian los datos antes de guardarlo a la db. 

            MultimediaContent multimedia_content = new MultimediaContent();
            return multimedia_content; 
        }

    }
}
