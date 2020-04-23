using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vivienda.Models.Nivel1;

namespace Vivienda.Controllers.Catalogos
{
    public class TipoViviendaController : BaseCatalogoNivel1DosCamposController
    {
        private static EstucturaNivel1C2 estructura = new EstucturaNivel1C2()
        {
            Id = "Id_TipoVivienda",
            Desc = "TipoVivienda",
            Desc2 = "Clave",
            Tabla = "Tbl_TipoVivienda"
        };
        private static Nivel1C2EstructuraPagina _pagina = new Nivel1C2EstructuraPagina()
        {
            Campo1 = "Tipo Vivienda",
            Estatus = "Estatus",
            MaxValue = 50,
            Titulo = "Tipo de vivienda",
            Estuctura = estructura,
            Controlador = "TipoVivienda",
            Campo2 = "Clave",
            MaxValue2 = 5
        };
        public TipoViviendaController() : base(_pagina)
        {
        }
    }
}
