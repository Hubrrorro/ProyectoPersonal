using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vivienda.Models.Nivel1
{
    public class Nivel1Model
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int Activo { get; set; }
        public string CtrEditar { get; set; }
        public string CtrEstatus { get; set; }
    }
}