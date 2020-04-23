using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vivienda.Models.Nivel1
{
    public class Nivel1EstructuraPagina
    {
        public Nivel1EstructuraPagina() {
        }
        public string Titulo { get; set; }
        public string Campo1 { get; set; }
        public int MaxValue { get; set; }
        public string Estatus { get; set; }
        public string Controlador { get; set; }

        public EstucturaNivel1 Estuctura { get; set; }
    }
}