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
    public class CondominioController : Controller
    //public class CondominioController : BaseController<Tbl_Condominio, CondominioModel>
    {
        //        public CondominioController() {
        //            this._objBD = new Tbl_Condominio();
        //            this._strModel = "Condominio";
        //            this._nombre = "condominio";
        //        }
        //        public override ActionResult Create() {
        //            this._objBD.Activo = false;
        //            return base.Create();
        //        }
        //        public override ActionResult Details(int id) {
        //            return base.Details(id);
        //        }
        //        public override ActionResult Search(FiltrosModel modelo)
        //        {
        //            this._parametros = new List<SqlParameter>() {
        //             new SqlParameter("@Condominio", modelo.Texto1),
        //             new SqlParameter("@Id_Condominio", modelo.Id),
        //             new SqlParameter("@Activo", modelo.Estatus)
        //            };
        //            _tabla = new TableModel();
        //            _tabla.ColumnaPost.Add(new ColumnaModel()
        //            {
        //                field = "Condominio",
        //                title = "Condominio"
        //            });
        //            _tabla.ColumnaPost.Add(new ColumnaModel()
        //            {
        //                field = "Clave",
        //                title = "Clave"
        //            });
        //            _tabla.ColumnaPost.Add(new ColumnaModel()
        //            {
        //                field = "CtrEstatus",
        //                title = "Estatus"
        //            });
        //            _tabla.ColumnaPost.Add(new ColumnaModel()
        //            {
        //                field = "CtrEditar",
        //                title = "Editar"
        //            });
        //            if (String.IsNullOrEmpty(modelo.Texto1))
        //               _parametros[0].Value = "";
        //            this._sp = "SP_Condominio_Consulta";
        //            return base.Search(modelo);
        //        }
        //    }
        //}


        public ActionResult Index()
        {
            return View();
        }

        // GET: Condominio/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.Tipo = "true";
            Tbl_Condominio condominio = new Tbl_Condominio();
            using (ViviendaEntities context = new ViviendaEntities())
            {
                condominio = context.Tbl_Condominio.Where(x => x.Id_Condominio.Equals(id)).FirstOrDefault();
            }
            ViewBag.Title = "Modificar condominio";
            return View("~/Views/Condominio/CreateMod.cshtml", condominio);
        }

        // GET: Condominio/Create
        public ActionResult Create()
        {
            ViewBag.Tipo = "false";
            ViewBag.Title = "Agregar condominio";
            Tbl_Condominio condominio = new Tbl_Condominio();
            condominio.Activo = false;
            return View("~/Views/Condominio/CreateMod.cshtml", condominio);
        }
        private RespuestaModel _respuesta;
        private List<CondominioModel> ObtenerInfo(FiltrosModel modelo)
        {
            GenericDAL<CondominioModel> CondominioDal = new GenericDAL<CondominioModel>();
            List<SqlParameter> parametros = new List<SqlParameter>() {
             new SqlParameter("@Condominio", modelo.Texto1),
             new SqlParameter("@Id_Condominio", modelo.Id),
             new SqlParameter("@Activo", modelo.Estatus)
            };
            if (String.IsNullOrEmpty(modelo.Texto1))
                parametros[0].Value = "";
            var listado = CondominioDal.Ejecuta("SP_Condominio_Consulta", parametros);
            return listado;
        }
        // POST: Condominio/Create
        [HttpGet]
        public ActionResult Search(FiltrosModel modelo)
        {
            _respuesta = new RespuestaModel();
            TableModel tabla = new TableModel();
            try
            {
                var listado = ObtenerInfo(modelo);
                foreach (var lstCondominio in listado)
                {
                    lstCondominio.CtrEditar = "<a href='/Condominio/Details?Id=" + lstCondominio.Id_Condominio.ToString() + "' class='btn btn-success btn-circle'><i class='fa fa-pencil'></i></a>";
                    if (lstCondominio.Activo.Equals(1))
                        lstCondominio.CtrEstatus = "<i class=\"fa fa-check-square-o\" aria-hidden=\"true\"></i> Activo";
                    else
                        lstCondominio.CtrEstatus = "<i class=\"fa fa-minus-square-o\" aria-hidden=\"true\"></i> Inactivo";
                }
                tabla.Datos = listado;
                tabla.ColumnaPost.Add(new ColumnaModel()
                {
                    field = "Condominio",
                    title = "Condominio"
                });
                tabla.ColumnaPost.Add(new ColumnaModel()
                {
                    field = "Clave",
                    title = "Clave"
                });
                tabla.ColumnaPost.Add(new ColumnaModel()
                {
                    field = "CtrEstatus",
                    title = "Estatus"
                });
                tabla.ColumnaPost.Add(new ColumnaModel()
                {
                    field = "CtrEditar",
                    title = "Editar"
                });
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
        public ActionResult Update(Tbl_Condominio modelo)
        {
            _respuesta = new RespuestaModel();
            try
            {
                using (ViviendaEntities context = new ViviendaEntities())
                {
                    var condominio = context.Tbl_Condominio.Where(x => x.Id_Condominio.Equals(modelo.Id_Condominio)).FirstOrDefault();
                    context.Entry(condominio).CurrentValues.SetValues(modelo);
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
        public ActionResult Create(Tbl_Condominio modelo)
        {
            _respuesta = new RespuestaModel();
            try
            {
                using (ViviendaEntities context = new ViviendaEntities())
                {
                    var cantidad = context.Tbl_Condominio.Where(x => x.Condominio.ToUpper().Equals(modelo.Condominio)).Count();
                    if (cantidad > 0)
                    {
                        _respuesta.Resultado = false;
                        _respuesta.Mensaje = "Se encuentra duplicado";
                        return Json(_respuesta);
                    }
                    cantidad = context.Tbl_Condominio.Where(x => x.Clave.ToUpper().Equals(modelo.Clave)).Count();
                    if (cantidad > 0)
                    {
                        _respuesta.Resultado = false;
                        _respuesta.Mensaje = "Se encuentra duplicado";
                        return Json(_respuesta);
                    }
                    modelo.Activo = true;
                    context.Tbl_Condominio.Add(modelo);
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
        public ActionResult Select()
        {
            _respuesta = new RespuestaModel();
            try
            {
                List<SelectModel> listado = new List<SelectModel>();
                var objmodel = ObtenerInfo(new FiltrosModel() { Estatus=1, Id=-1, Texto1 = "", Texto2 ="" });
                foreach (var model in objmodel)
                {
                    listado.Add(new SelectModel() { Id = model.Id_Condominio, Descripcion = model.Condominio });
                }
                _respuesta.Resultado = true;
                _respuesta.Datos = listado;
            }
            catch (Exception ex)
            {
                _respuesta.Resultado = false;
                _respuesta.Mensaje = Resources.Recursos.MsnError;
            }
            return Json(_respuesta, JsonRequestBehavior.AllowGet);
        }
    }
}
