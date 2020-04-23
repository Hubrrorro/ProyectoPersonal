using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vivienda.Models.BD
{
    public class PropietarioModel
    {
        public int Id_Propietario { get; set; }
        public int Id_Vivienda { get; set; }
        public string Nombre { get; set; }
        public string ApePaterno { get; set; }
        public string ApeMaterno { get; set; }
        public string Correo { get; set; }
        public string Tel1 { get; set; }
        public string Tel2 { get; set; }
        public string Cel1 { get; set; }
        public string Cel2 { get; set; }
        public int Activo { get; set; }
        public string Condominio { get; set; }
        public string Vivienda { get; set; }
        public string NumExt { get; set; }
        public string NumInt { get; set; }
    }
}