using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vivienda.Models.BD
{
    public class FiltrosModel
    {
        public int Id { get; set; }
        public string Texto1 { get; set; }
        public string Texto2 { get; set; }
        public int Estatus { get; set; }
    }
}