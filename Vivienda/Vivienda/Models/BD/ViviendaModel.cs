using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vivienda.Models.BD
{
    public class ViviendaModel
    {
        public int Id_Vivienda { get; set; }
        public string Vivienda { get; set; }
        public int Id_TipoVivienda { get; set; }
        public int Id_Condominio { get; set; }
        public string Condominio { get; set; }
        public int Activo { get; set; }
        public int ActivoPropietario { get; set; }
        public string CtrEditar { get; set; }
        public string CtrEstatus { get; set; }
        public string CtrPropietario { get; set; }
        public string CtrPropietarioEditar { get; set; }
        public string Calle { get; set; }
        public string Lote { get; set; }
        public string NumExt { get; set; }
        public string NumInt { get; set; }
    }
}