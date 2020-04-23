using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vivienda.Models.Nivel1;

namespace Vivienda.Controllers.Catalogos
{
    public class EstadosController : BaseCatalogoNivel1Controller
    {
        private static EstucturaNivel1 estructura = new EstucturaNivel1()
        {
            Id = "Id_Estado",
            Desc = "Estado",
            Tabla = "Tbl_Estado"
        };
        private static Nivel1EstructuraPagina _pagina = new Nivel1EstructuraPagina() {
            Campo1 = "Estado",
            Estatus = "Estatus",
            MaxValue = 50,
            Titulo = "Estado",
            Estuctura = estructura,
            Controlador = "Estados"
        };
        public EstadosController() : base(_pagina)
        { 
        }
    }
}
