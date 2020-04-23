using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vivienda.Interface;

namespace Vivienda.Models.BD
{
    public class CondominioModel
    {
        public int Id_Condominio { get; set; }
        public string Condominio { get; set; }
        public string Clave { get; set; }
        public string Colonia { get; set; }
        public int Id_Estado { get; set; }
        public int CP { get; set; }
        public string Municipio { get; set; }
        public int Activo { get; set; }
        public string CtrEditar { get; set; }
        public string CtrEstatus { get; set; }
    }
}