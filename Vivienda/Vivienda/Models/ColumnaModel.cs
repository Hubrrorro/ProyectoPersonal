using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vivienda.Models
{
    public class ColumnaModel
    {
        public ColumnaModel()
        {
            this.aling = "center";
            this.sortable = true;
            this.valign = "middle";
            this.view = true;
        }
        public string field { get; set; }
        public string title { get; set; }
        public string aling { get; set; }
        public string valign { get; set; }
        public bool sortable { get; set; }
        public string footerFormatter { get; set; }
        public bool view { get; set; }
    }
}