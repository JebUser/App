﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMCAPI.Core.DTO
{
    public class TipobeneDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public TipobeneDto(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }
    }
}
