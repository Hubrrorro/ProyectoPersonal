using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vivienda.DAL;
using Vivienda.Models;
using Vivienda.Models.BD;

namespace Vivienda.Controllers
{
    public class ViviendaController : Controller
    {
        public ActionResult Index()
        {
            GenericDAL<CondominioModel> CondominioDal = new GenericDAL<CondominioModel>();
            int idCondominio = 0;
            List<SqlParameter> parametros = new List<SqlParameter>() {
             new SqlParameter("@Condominio", ""),
             new SqlParameter("@Id_Condominio", idCondominio),
             new SqlParameter("@Activo", 1)
            };
            
                var listado = CondominioDal.Ejecuta("SP_Condominio_Consulta", parametros);
                return View(listado);
        }

        // GET: Condominio/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.Tipo = "true";
            Tbl_Vivienda vivienda = new Tbl_Vivienda();
            using (ViviendaEntities context = new ViviendaEntities())
            {
                vivienda = context.Tbl_Vivienda.Where(x => x.Id_Vivienda.Equals(id)).FirstOrDefault();
            }
            ViewBag.Title = "Modificar Vivienda";
            return View("~/Views/Vivienda/CreateMod.cshtml", vivienda);
        }
        public ActionResult Propietario(int id)
        {
            GenericDAL<PropietarioModel> propietarioDal = new GenericDAL<PropietarioModel>();
            List<SqlParameter> parametros = new List<SqlParameter>() {
             new SqlParameter("@Id_Vivienda", id),
             new SqlParameter("@Id_Propietario", -1)
            };

            var listado = propietarioDal.Ejecuta("SP_Propietario_Consultar", parametros).FirstOrDefault();
            return View("~/Views/Vivienda/Propietario.cshtml", listado);
        }
        // GET: Condominio/Create
        public ActionResult Create()
        {
            ViewBag.Tipo = "false";
            ViewBag.Title = "Agregar Vivienda";
            Tbl_Vivienda vivienda = new Tbl_Vivienda() { 
            Activo= false
            };
            return View("~/Views/Vivienda/CreateMod.cshtml", vivienda);
        }
        private RespuestaModel _respuesta;
        private List<ViviendaModel> ObtenerInfo(FiltrosModel modelo, int IdVivienda) {
            GenericDAL<ViviendaModel> viviendaDal = new GenericDAL<ViviendaModel>();
            List<SqlParameter> parametros = new List<SqlParameter>() {
             new SqlParameter("@Vivienda", modelo.Texto1),
             new SqlParameter("@Id_Vivienda", IdVivienda),
              new SqlParameter("@Activo", modelo.Estatus),
             new SqlParameter("@Id_Condominio", modelo.Id)
            };
            if (String.IsNullOrEmpty(modelo.Texto1))
                parametros[0].Value = "";
            var listado = viviendaDal.Ejecuta("SP_Vivienda_Consulta", parametros);
            return listado;
        }
        // POST: Condominio/Create
        [HttpGet]
        public ActionResult Search(FiltrosModel modelo)
        {
            _respuesta = new RespuestaModel();
            
            int IdVivienda = -1;
            

            TableModel tabla = new TableModel();
            try
            {
                var listado = ObtenerInfo(modelo, IdVivienda);
                    foreach (var lstVivienda in listado)
                {
                    lstVivienda.CtrEditar = "<a href='/Vivienda/Details?Id=" + lstVivienda.Id_Vivienda.ToString() + "' class='btn btn-success btn-circle'><i class='fa fa-pencil'></i></a>";
                    lstVivienda.CtrPropietarioEditar = "<a href='/Vivienda/Propietario?Id=" + lstVivienda.Id_Vivienda.ToString() + "' class='btn btn-success btn-circle'><i class='fa fa-user'></i></a>";
                    lstVivienda.CtrEditar += "&nbsp&nbsp&nbsp" + lstVivienda.CtrPropietarioEditar;
                    //CtrPropietarioEditar
                    if (lstVivienda.Activo.Equals(1))
                        lstVivienda.CtrEstatus = "<i class=\"fa fa-check-square-o\" aria-hidden=\"true\"></i> Activo";
                    else
                        lstVivienda.CtrEstatus = "<i class=\"fa fa-minus-square-o\" aria-hidden=\"true\"></i> Inactivo";

                    if (lstVivienda.ActivoPropietario.Equals(1))
                        lstVivienda.CtrPropietario = "<i class=\"fa fa-check-square-o\" aria-hidden=\"true\"></i> Propietario";
                    else
                        lstVivienda.CtrPropietario = "<i class=\"fa fa-minus-square-o\" aria-hidden=\"true\"></i> Sin propietario";
                }
                tabla.Datos = listado;
                tabla.ColumnaPost.Add(new ColumnaModel()
                {
                    field = "Vivienda",
                    title = "Vivienda"
                });
                tabla.ColumnaPost.Add(new ColumnaModel()
                {
                    field = "Calle",
                    title = "Calle"
                });
                tabla.ColumnaPost.Add(new ColumnaModel()
                {
                    field = "NumExt",
                    title = "Número Ext."
                });
                tabla.ColumnaPost.Add(new ColumnaModel()
                {
                    field = "Condominio",
                    title = "Condominio"
                });
                tabla.ColumnaPost.Add(new ColumnaModel()
                {
                    field = "CtrEstatus",
                    title = "Estatus"
                });
                tabla.ColumnaPost.Add(new ColumnaModel()
                {
                    field = "CtrPropietario",
                    title = "Propietario"
                });
                tabla.ColumnaPost.Add(new ColumnaModel()
                {
                    field = "CtrEditar",
                    title = "Acciones"
                });
                //tabla.ColumnaPost.Add(new ColumnaModel()
                //{
                //    field = "CtrPropietarioEditar",
                //    title = "Editar Propietario"
                //});
                _respuesta.Datos = tabla;
                _respuesta.Resultado = true;
            }
            catch (Exception ex)
            {
                _respuesta.Resultado = false;
                _respuesta.Mensaje = ex.Message;
            }
            return Json(_respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpPut]
        public ActionResult Update(Tbl_Vivienda modelo)
        {
            _respuesta = new RespuestaModel();
            try
            {
                using (ViviendaEntities context = new ViviendaEntities())
                {
                    var tipoVivienda = context.Tbl_TipoVivienda.Where(x => x.Id_TipoVivienda.Equals(modelo.Id_TipoVivienda)).FirstOrDefault().Clave;
                    var condominio = context.Tbl_Condominio.Where(x => x.Id_Condominio.Equals(modelo.Id_Condominio)).FirstOrDefault().Clave;
                    modelo.Activo = true;
                    modelo.Vivienda = condominio + "-" + tipoVivienda + "-" + modelo.NumExt;
                    if (!String.IsNullOrEmpty(modelo.NumInt))
                    {
                        modelo.Vivienda += " " + modelo.NumInt;
                    }
                    if (String.IsNullOrEmpty(modelo.NumInt))
                    {
                        modelo.NumInt = "";
                    }
                    if (String.IsNullOrEmpty(modelo.Lote))
                    {
                        modelo.Lote = "";
                    }
                    var vivienda = context.Tbl_Vivienda.Where(x => x.Id_Vivienda.Equals(modelo.Id_Vivienda)).FirstOrDefault();
                    context.Entry(vivienda).CurrentValues.SetValues(modelo);
                    context.SaveChanges();
                    _respuesta.Mensaje = "Se actualizó correctamente";
                    _respuesta.Resultado = true;

                }
            }
            catch (Exception ex)
            {
                _respuesta.Resultado = false;
                _respuesta.Mensaje = ex.Message;
            }
            return Json(_respuesta);
        }

        [HttpPost]
        public ActionResult Create(Tbl_Vivienda modelo)
        {
            _respuesta = new RespuestaModel();
            try
            {
                using (ViviendaEntities context = new ViviendaEntities())
                {
                    var cantidad = context.Tbl_Vivienda.Where(x => x.Id_Condominio.Equals(modelo.Id_Condominio) 
                    && x.NumExt.Trim().Equals(modelo.NumExt.Trim())
                    && x.Id_TipoVivienda.Equals(modelo.Id_TipoVivienda)).Count();
                    if (cantidad > 0)
                    {
                        _respuesta.Resultado = false;
                        _respuesta.Mensaje = "Se encuentra duplicado";
                        return Json(_respuesta);
                    }
                    var tipoVivienda = context.Tbl_TipoVivienda.Where(x => x.Id_TipoVivienda.Equals(modelo.Id_TipoVivienda)).FirstOrDefault().Clave;
                    var condominio = context.Tbl_Condominio.Where(x => x.Id_Condominio.Equals(modelo.Id_Condominio)).FirstOrDefault().Clave;
                    modelo.Activo = true;
                    
                    modelo.Vivienda = condominio + "-" + tipoVivienda + "-" + modelo.NumExt;
                    if (!String.IsNullOrEmpty(modelo.NumInt))
                    {
                        modelo.Vivienda += " " + modelo.NumInt;
                    }
                    if (String.IsNullOrEmpty(modelo.NumInt))
                    {
                        modelo.NumInt = "";
                    }
                    if (String.IsNullOrEmpty(modelo.Lote))
                    {
                        modelo.Lote = "";
                    }
                    context.Tbl_Vivienda.Add(modelo);
                    context.SaveChanges();
                    _respuesta.Mensaje = "Se guardó correctamente";
                    _respuesta.Resultado = true;

                }
            }
            catch (Exception ex)
            {
                _respuesta.Resultado = false;
                _respuesta.Mensaje = ex.Message;
            }
            return Json(_respuesta);
        }

        [HttpGet]
        public ActionResult DetailPropietario(int id)
        {
            try
            {
                _respuesta = new RespuestaModel();
                GenericDAL<PropietarioModel> propietarioDal = new GenericDAL<PropietarioModel>();
                List<SqlParameter> parametros = new List<SqlParameter>() {
             new SqlParameter("@Id_Vivienda", id),
             new SqlParameter("@Id_Propietario", -1)
            };

                var listado = propietarioDal.Ejecuta("SP_Propietario_Consultar", parametros).FirstOrDefault();
                _respuesta.Datos = listado;
                _respuesta.Resultado = true;
            }
            catch (Exception ex)
            {
                _respuesta.Resultado = false;
                _respuesta.Mensaje = ex.Message;
            }
            return Json(_respuesta);
        }
        [HttpPut]
        public ActionResult UpdatePropietario(PropietarioModel propietario)
        {

            try
            {
                _respuesta = new RespuestaModel();
                GenericDAL<PropietarioModel> propietarioDal = new GenericDAL<PropietarioModel>();
                List<SqlParameter> parametros = new List<SqlParameter>() {
             new SqlParameter("@Nombre", propietario.Nombre),
             new SqlParameter("@ApePaterno", propietario.ApePaterno),
             new SqlParameter("@ApeMaterno", propietario.ApeMaterno),
             new SqlParameter("@Correo", propietario.Correo),
             new SqlParameter("@Tel1", String.IsNullOrEmpty(propietario.Tel1)? "" : propietario.Tel1),
             new SqlParameter("@Tel2", String.IsNullOrEmpty(propietario.Tel2)? "" : propietario.Tel2),
             new SqlParameter("@Cel1",  String.IsNullOrEmpty(propietario.Cel1)? "" : propietario.Cel1),
             new SqlParameter("@Cel2",  String.IsNullOrEmpty(propietario.Cel2)? "" : propietario.Cel2),
             new SqlParameter("@Activo", propietario.Activo),
             new SqlParameter("@Id_Vivienda", propietario.Id_Vivienda),
            };

                var listado = propietarioDal.Ejecuta("SP_Propietario_Modificar", parametros).FirstOrDefault();
                _respuesta.Datos = listado;
                _respuesta.Resultado = true;
                _respuesta.Mensaje = Resources.Recursos.MnsActualizar;
            }
            catch (Exception ex)
            {
                _respuesta.Resultado = false;
                _respuesta.Mensaje = ex.Message;
            }
            return Json(_respuesta);
        }
    }
}
