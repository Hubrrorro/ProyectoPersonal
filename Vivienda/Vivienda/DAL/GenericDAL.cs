using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Vivienda.DAL
{
    public class GenericDAL<T> where T : class
    {
        private string _strQry = string.Empty;
        /// <summary>
        /// Método para preparar el query para su ejecución.
        /// </summary>
        /// <param name="sp">Nombre del Stored Procedure que se va a ejecutar</param>
        /// <param name="parametros">Listado de parametros en Sql que solicita el Stored Procedure</param>
        /// <returns></returns>
        private string PrepararConsulta(string sp, List<SqlParameter> parametros)
        {
            _strQry += "EXEC " + sp;
            foreach (SqlParameter parametro in parametros)
            {
                _strQry += " " + parametro.ParameterName + ",";
            }
            if (parametros.Count() > 0)
                _strQry = _strQry.Substring(0, _strQry.Length - 1);
            return _strQry;
        }
        /// <summary>
        /// Ejecucion del stored procedure
        /// </summary>
        /// <param name="sp">Nombre del stored procedure que se van a ejecutar</param>
        /// <param name="parametros">Listado de paremetros en Sql que solicita el Stored Procedure</param>
        /// <returns>Listado de Clase(Modelo) que se solicitó al inicio de la clase</returns>
        public List<T> Ejecuta(string sp, List<SqlParameter> parametros)
        {
            using (var varConn = new ViviendaEntities())
            {
                List<T> listado = new List<T>();
                try
                {
                    varConn.Database.Connection.Open();
                    string qry = PrepararConsulta(sp, parametros);
                    listado = varConn.Database.SqlQuery<T>(qry, parametros.ToArray()).ToList();
                    varConn.Database.Connection.Close();
                }
                catch (Exception ex)
                {
                    if (varConn.Database.Connection.State == System.Data.ConnectionState.Open)
                    {
                        varConn.Database.Connection.Close();
                    }
                    throw new Exception(ex.Message);
                }
                return listado;
            }
        }
    }
}
