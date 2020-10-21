﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;
using System.Text;

namespace PRIME_UCR.Domain.Models
{
    public class MultimediaContent
    {
        [Key]
        public int ID { get; set; }

        public string Nombre { get; set; }

        public string Archivo { get; set; }

        public string Descripcion { get; set; }

        public DateTime Fecha_Hora { get; set; }

        public string Tipo { get; set; }

        public int ID_accion { get; set; }

        //a relationship will be created when there is a navigation property discovered on a type. 
        //public Acciones Acciones { get; set; }

        //public string ID_listaDeChequeo { get; set; } Este esta pendiente. 
    }
}
