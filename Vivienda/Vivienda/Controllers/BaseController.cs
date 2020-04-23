using Resources;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vivienda.DAL;
using Vivienda.Interface;
using Vivienda.Models;
using Vivienda.Models.BD;

namespace Vivienda.Controllers
{
    public class BaseController<T, C> : Controller where T : class where C : class , IModelSelDB
    {
        
        public string _strModel { get; set; }
        public T _objBD { get; set; }
        public List<SqlParameter> _parametros { get; set; }
        public string _sp {get;set;}
        public TableModel _tabla { get; set; }
        public string _nombre { get; set; }
        public ActionResult Index()
        {
            return View();
        }

        // GET: Condominio/Details/5
        public virtual ActionResult Details(int id)
        {
            ViewBag.Tipo = "true";
            using (ViviendaEntities context = new ViviendaEntities())
            {
                var dbSet = context.Set<T>();
                // _objBD =dbSet.FirstOrDefault(x => x.Id.Equals(id));
                _objBD = dbSet.Find(id);
            }
            ViewBag.Title = "Modificar "+ _nombre;
            return View("~/Views/"+ _strModel + "/CreateMod.cshtml", _objBD);
        }

        // GET: Condominio/Create
        public virtual ActionResult Create()
        {
            ViewBag.Tipo = "false";
            ViewBag.Title = "Agregar " + _nombre;
            return View("~/Views/" + _strModel + "/CreateMod.cshtml", _objBD);
        }
        private RespuestaModel _respuesta;
        // POST: Condominio/Create
        [HttpGet]
        public virtual ActionResult Search(FiltrosModel modelo)
        {
            _respuesta = new RespuestaModel();
            GenericDAL<C> dal = new GenericDAL<C>();
            try
            {
                var listado = dal.Ejecuta(_sp, _parametros);
                foreach (var lstCondominio in listado)
                {
                    lstCondominio.CtrEditar = "<a href='/Condominio/Details?Id=" + lstCondominio.Id.ToString() + "' class='btn btn-success btn-circle'><i class='fa fa-pencil'></i></a>";
                    if (lstCondominio.Activo.Equals(1))
                        lstCondominio.CtrEstatus = "<i class=\"fa fa-check-square-o\" aria-hidden=\"true\"></i> Activo";
                    else
                        lstCondominio.CtrEstatus = "<i class=\"fa fa-minus-square-o\" aria-hidden=\"true\"></i> Inactivo";
                }
                _tabla.Datos = listado;
                _respuesta.Datos = _tabla;
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
        public ActionResult Update(T modelo)
        {
            _respuesta = new RespuestaModel();
            try
            {
                using (ViviendaEntities context = new ViviendaEntities())
                {
                    var dbSet = context.Set<T>();
                    //_objBD = dbSet.FirstOrDefault(x => x.Id.Equals(modelo.Id));
                    _objBD = dbSet.Find(modelo);
                    context.Entry(_objBD).CurrentValues.SetValues(modelo);
                    context.SaveChanges();
                    _respuesta.Mensaje = Recursos.MnsActualizar;
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
        public ActionResult Create(T modelo)
        {
            _respuesta = new RespuestaModel();
            try
            {
                using (ViviendaEntities context = new ViviendaEntities())
                {
                    //var cantidad = context.Tbl_Condominio.Where(x => x.Condominio.ToUpper().Equals(modelo.Condominio)).Count();
                    //if (cantidad > 0)
                    //{
                    //    _respuesta.Resultado = false;
                    //    _respuesta.Mensaje = "Se encuentra duplicado";
                    //    return Json(_respuesta);
                    //}
                    //cantidad = context.Tbl_Condominio.Where(x => x.Clave.ToUpper().Equals(modelo.Clave)).Count();
                    //if (cantidad > 0)
                    //{
                    //    _respuesta.Resultado = false;
                    //    _respuesta.Mensaje = "Se encuentra duplicado";
                    //    return Json(_respuesta);
                    //}
                    //modelo.Activo = true;
                    var dbSet = context.Set<T>();
                    dbSet.Add(modelo);
                    context.SaveChanges();
                    _respuesta.Mensaje = Recursos.MnsGuardar;
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


    }
}
