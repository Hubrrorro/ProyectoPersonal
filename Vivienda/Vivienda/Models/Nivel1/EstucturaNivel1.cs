using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vivienda.Models.Nivel1
{
    public class EstucturaNivel1
    {
        public string Id { get; set; }
		public string Desc { get; set; }
		public string Tabla { get; set; }
		public int IdValor { get; set; }
		public string DescValor { get; set; }
		public int EstatusValor { get; set; }
    }
	public class EstucturaNivel1C2 : EstucturaNivel1
	{
		public string Desc2 { get; set; }
		public string DescValor2 { get; set; }
	}
}