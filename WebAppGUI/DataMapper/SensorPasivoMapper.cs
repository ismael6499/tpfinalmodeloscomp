using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Data;
using WebAppGUI.Modelos;

namespace WebAppGUI.DataMapper
{
    public class SensorPasivoMapper
    {
        private readonly SensorPasivoData db;

        public SensorPasivoMapper()
        {
            db = SensorPasivoData.GetInstance();
        }

        public void Agregar(SensorPasivo model)
        {
            var sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@Nombre", model.Nombre);
            sqlParameters[1] = new SqlParameter("@Url", model.Url);
            sqlParameters[2] = new SqlParameter("@Conectado", model.Conectado);

            db.Escribir("agregar", sqlParameters);
        }

        public void Actualizar(SensorPasivo model)
        {
            var sqlParameters = new SqlParameter[6];
            sqlParameters[0] = new SqlParameter("@Id", model.Id);
            sqlParameters[1] = new SqlParameter("@Nombre", model.Nombre);
            sqlParameters[2] = new SqlParameter("@Url", model.Url);
            sqlParameters[3] = new SqlParameter("@Conectado", model.Conectado);
            sqlParameters[4] = new SqlParameter("@Libre", model.Libre);
            sqlParameters[5] = new SqlParameter("@UltimaActividad", model.UltimaActividad);

            db.Escribir("agregar", sqlParameters);
        }

        public List<SensorPasivo> Listar()
        {
            
            var lista = new List<SensorPasivo>();
            var tabla = db.Consultar("getAll", null);
            foreach (DataRow fila in tabla.Rows)
            {
                var SensorPasivo = new SensorPasivo();
                SensorPasivo.Id = (int) fila["Id"];
                SensorPasivo.Nombre = fila["Nombre"].ToString();
                SensorPasivo.Url= fila["Url"].ToString();
                SensorPasivo.Conectado = (bool) fila["Conectado"];
                SensorPasivo.Libre = (bool) fila["Libre"];
                SensorPasivo.UltimaActividad =  DateTime.Parse(fila["UltimaActividad"].ToString());
                lista.Add(SensorPasivo);
            }

            return lista;
        }

        public SensorPasivo Get(int id)
        {
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@Id", id);

            var tabla = db.Consultar("get", sqlParameters);
            foreach (DataRow fila in tabla.Rows)
            {
                var SensorPasivo = new SensorPasivo();
                SensorPasivo.Id = (int) fila["Id"];
                SensorPasivo.Nombre = fila["Nombre"].ToString();
                SensorPasivo.Url= fila["Url"].ToString();
                SensorPasivo.Conectado = (bool) fila["Conectado"];
                SensorPasivo.Libre = (bool) fila["Libre"];
                SensorPasivo.UltimaActividad =  DateTime.Parse(fila["UltimaActividad"].ToString());
                return SensorPasivo;
            }

            return null;
        }

        public void Eliminar(SensorPasivo model)
        {
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@Id", model.Id);

            db.Escribir("remove", sqlParameters);
        }

    }
}
