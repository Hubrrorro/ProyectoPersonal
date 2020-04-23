using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vivienda.DAL;
using Vivienda.Models;
using Vivienda.Models.BD;
using Vivienda.Models.Nivel1;

namespace Vivienda.Controllers.Catalogos
{
    public class BaseCatalogoNivel1Controller : Controller
    {
        private Nivel1EstructuraPagina _pagina;
        private RespuestaModel _respuesta;
        public BaseCatalogoNivel1Controller(Nivel1EstructuraPagina pagina) {
            _pagina = pagina;
        }
        // GET: BaseCatalogoNivel1
        public ActionResult Index()
        {
            ViewBag.Title = _pagina.Titulo;
            ViewBag.Max = _pagina.MaxValue;
            ViewBag.Campo1 = _pagina.Campo1;
            return View(@"~/Views/BaseCatalogoNivel1/Index.cshtml");
        }
        // GET: BaseCatalogoNivel1/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.Tipo = "true";
            GenericDAL<Nivel1Model> objModelDal = new GenericDAL<Nivel1Model>();
            List<SqlParameter> parametros = new List<SqlParameter>() {
             new SqlParameter("@id", _pagina.Estuctura.Id),
             new SqlParameter("@desc", _pagina.Estuctura.Desc),
             new SqlParameter("@tabla", _pagina.Estuctura.Tabla),
             new SqlParameter("@idValor",id),
             new SqlParameter("@descValor", ""),
             new SqlParameter("@estatusValor", -1),
            };
            var objmodel = objModelDal.Ejecuta("SP_Nivel1_Consultar", parametros).FirstOrDefault();
            ViewBag.Title = _pagina.Titulo;
            ViewBag.Max = _pagina.MaxValue;
            ViewBag.Campo1 = _pagina.Campo1;
            return View("~/Views/BaseCatalogoNivel1/CreateMod.cshtml", objmodel);
        }

        // GET: BaseCatalogoNivel1/Create
        public ActionResult Create()
        {
            ViewBag.Tipo = "false";
            ViewBag.Title = _pagina.Titulo;
            ViewBag.Max = _pagina.MaxValue;
            ViewBag.Campo1 = _pagina.Campo1;
            Nivel1Model modelo = new Nivel1Model();
            modelo.Activo = 1;
            return View("~/Views/BaseCatalogoNivel1/CreateMod.cshtml", modelo);
        }

        [HttpGet]
        public ActionResult Search(FiltrosModel modelo)
        {
            _respuesta = new RespuestaModel();
            GenericDAL<Nivel1Model> objModelDal = new GenericDAL<Nivel1Model>();
            List<SqlParameter> parametros = new List<SqlParameter>() {
             new SqlParameter("@id", _pagina.Estuctura.Id),
             new SqlParameter("@desc", _pagina.Estuctura.Desc),
             new SqlParameter("@tabla", _pagina.Estuctura.Tabla),
             new SqlParameter("@idValor", modelo.Id),
             new SqlParameter("@descValor", modelo.Texto1),
             new SqlParameter("@estatusValor", modelo.Estatus),
            };
            if (String.IsNullOrEmpty(modelo.Texto1))
                parametros[4].Value = "";

            
            TableModel tabla = new TableModel();
            try
            {
                var listado = objModelDal.Ejecuta("SP_Nivel1_Consultar", parametros);
                foreach (var lstCondominio in listado)
                {
                    lstCondominio.CtrEditar = "<a href='/"+ _pagina.Controlador + "/Details?Id=" + lstCondominio.Id.ToString() + "' class='btn btn-success btn-circle'><i class='fa fa-pencil'></i></a>";
                    if (lstCondominio.Activo.Equals(1))
                        lstCondominio.CtrEstatus = "<i class=\"fa fa-check-square-o\" aria-hidden=\"true\"></i> Activo";
                    else
                        lstCondominio.CtrEstatus = "<i class=\"fa fa-minus-square-o\" aria-hidden=\"true\"></i> Inactivo";
                }
                tabla.Datos = listado;
                tabla.ColumnaPost.Add(new ColumnaModel()
                {
                    field = "Descripcion",
                    title = _pagina.Campo1
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
        public ActionResult Update(Nivel1Model modelo)
        {
            _respuesta = new RespuestaModel();
            try
            {
                if (!Duplicado(modelo))
                {
                    _respuesta.Resultado = false;
                    _respuesta.Mensaje = Resources.Recursos.MnsDuplicado;
                    return Json(_respuesta);
                }
                GenericDAL<Nivel1Model> objModelDal = new GenericDAL<Nivel1Model>();
                List<SqlParameter> parametros = new List<SqlParameter>() {
                 new SqlParameter("@id", _pagina.Estuctura.Id),
                 new SqlParameter("@desc", _pagina.Estuctura.Desc),
                 new SqlParameter("@tabla", _pagina.Estuctura.Tabla),
                 new SqlParameter("@idValor", modelo.Id),
                 new SqlParameter("@descValor", modelo.Descripcion),
                 new SqlParameter("@estatusValor", modelo.Activo),
                };
                objModelDal.Ejecuta("SP_Nivel1_Modificar", parametros);
                _respuesta.Resultado = true;
                _respuesta.Mensaje = Resources.Recursos.MnsActualizar;
            }
            catch (Exception ex)
            {
                _respuesta.Resultado = false;
                _respuesta.Mensaje = Resources.Recursos.MsnError;
            }
            return Json(_respuesta);
        }
        private bool Duplicado(Nivel1Model modelo){
            GenericDAL<Nivel1Model> objModelDal = new GenericDAL<Nivel1Model>();
            List<SqlParameter> parametros = new List<SqlParameter>() {
             new SqlParameter("@id", _pagina.Estuctura.Id),
             new SqlParameter("@desc", _pagina.Estuctura.Desc),
             new SqlParameter("@tabla", _pagina.Estuctura.Tabla),
             new SqlParameter("@idValor", -1),
             new SqlParameter("@descValor", ""),
             new SqlParameter("@estatusValor", -1),
            };
            var listado = objModelDal.Ejecuta("SP_Nivel1_Consultar", parametros);
            int existe;
            if (modelo.Id.Equals(0))
                existe  = listado.Where(x => x.Descripcion.ToUpper().Trim() == modelo.Descripcion.ToUpper().Trim()).Count();
            else
                existe = listado.Where(x => x.Descripcion.ToUpper().Trim() == modelo.Descripcion.ToUpper().Trim() && x.Id != modelo.Id).Count();
            return existe.Equals(0);
        }
        [HttpPost]
        public ActionResult Create(Nivel1Model modelo)
        {
            _respuesta = new RespuestaModel();
            try
            {
                if (!Duplicado(modelo))
                {
                    _respuesta.Resultado = false;
                    _respuesta.Mensaje = Resources.Recursos.MnsDuplicado;
                    return Json(_respuesta);
                }
                GenericDAL<Nivel1Model> objModelDal = new GenericDAL<Nivel1Model>();
                List<SqlParameter> parametros = new List<SqlParameter>() {
                 new SqlParameter("@desc", _pagina.Estuctura.Desc),
                 new SqlParameter("@tabla", _pagina.Estuctura.Tabla),
                 new SqlParameter("@descValor", modelo.Descripcion)
                };
                objModelDal.Ejecuta("SP_Nivel1_Agregar", parametros);
                _respuesta.Resultado = true;
                _respuesta.Mensaje = Resources.Recursos.MnsGuardar;
            }
            catch (Exception ex)
            {
                _respuesta.Resultado = false;
                _respuesta.Mensaje = Resources.Recursos.MsnError;
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
                GenericDAL<Nivel1Model> objModelDal = new GenericDAL<Nivel1Model>();
                List<SqlParameter> parametros = new List<SqlParameter>() {
                 new SqlParameter("@id", _pagina.Estuctura.Id),
                 new SqlParameter("@desc", _pagina.Estuctura.Desc),
                 new SqlParameter("@tabla", _pagina.Estuctura.Tabla),
                 new SqlParameter("@idValor",-1),
                 new SqlParameter("@descValor", ""),
                 new SqlParameter("@estatusValor", 1)
            };
                var objmodel = objModelDal.Ejecuta("SP_Nivel1_Consultar", parametros);
                foreach (var model in objmodel)
                {
                    listado.Add(new SelectModel() { Id = model.Id, Descripcion = model.Descripcion });
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

