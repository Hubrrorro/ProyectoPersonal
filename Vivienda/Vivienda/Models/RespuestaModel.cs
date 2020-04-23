using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vivienda.Models
{
    public class RespuestaModel
    {
        public bool Resultado { get; set; }
        public string Mensaje { get; set; }
        public dynamic Datos { get; set; }

    }
}