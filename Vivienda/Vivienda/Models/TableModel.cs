using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vivienda.Models
{
    public class TableModel
    {
        public TableModel()
        {
            this.Atributos = new List<AtributosModel>();
            this.ColumnaPost = new List<ColumnaModel>();
            this.Atributos.Add(new AtributosModel() { nombre = "data-show-export", valor = "true" });
            this.Atributos.Add(new AtributosModel() { nombre = "data-height", valor = "700" });
            this.Atributos.Add(new AtributosModel() { nombre = "data-show-fullscreen", valor = "true" });
            this.Atributos.Add(new AtributosModel() { nombre = "data-pagination", valor = "true" });
            this.Atributos.Add(new AtributosModel() { nombre = "data-only-info-pagination", valor = "true" });
            this.Atributos.Add(new AtributosModel() { nombre = "data-search", valor = "true" });
            this.Atributos.Add(new AtributosModel() { nombre = "data-show-footer", valor = "true" });
            this.Atributos.Add(new AtributosModel() { nombre = "data-resizable", valor = "true" });
            this.Atributos.Add(new AtributosModel() { nombre = "data-toggle", valor = "table" });

        }
        public dynamic Datos { get; set; }
        public List<AtributosModel> Atributos { get; set; }
        public List<ColumnaModel> ColumnaPost { get; set; }
    }
}