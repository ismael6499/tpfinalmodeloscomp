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
    public class SensorActivoMapper
    {
        private readonly SensorActivoData db;

        public SensorActivoMapper()
        {
            db = SensorActivoData.GetInstance();
        }

        public void Agregar(SensorActivo model)
        {
            var sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@Nombre", model.Nombre);
            sqlParameters[1] = new SqlParameter("@Url", model.Url);
            sqlParameters[2] = new SqlParameter("@Conectado", model.Conectado);
            sqlParameters[3] = new SqlParameter("@Encendido", model.Encendido);

            db.Escribir("agregar", sqlParameters);
        }

        public void Actualizar(SensorActivo model)
        {
            var sqlParameters = new SqlParameter[6];
            sqlParameters[0] = new SqlParameter("@Id", model.Id);
            sqlParameters[1] = new SqlParameter("@Nombre", model.Nombre);
            sqlParameters[2] = new SqlParameter("@Url", model.Url);
            sqlParameters[3] = new SqlParameter("@Conectado", model.Conectado);
            sqlParameters[4] = new SqlParameter("@Encendido", model.Encendido);
            sqlParameters[5] = new SqlParameter("@Estado", model.Estado);
            sqlParameters[6] = new SqlParameter("@UltimaActividad", model.UltimaActividad);

            db.Escribir("agregar", sqlParameters);
        }

        public List<SensorActivo> Listar()
        {
            
            var lista = new List<SensorActivo>();
            var tabla = db.Consultar("getAll", null);
            foreach (DataRow fila in tabla.Rows)
            {
                var SensorActivo = new SensorActivo();
                SensorActivo.Id = (int) fila["Id"];
                SensorActivo.Nombre = fila["Nombre"].ToString();
                SensorActivo.Url= fila["Url"].ToString();
                SensorActivo.Conectado = (bool) fila["Conectado"];
                SensorActivo.Encendido = (bool) fila["Encendido"];
                SensorActivo.Estado = fila["Estado"].ToString();
                SensorActivo.UltimaActividad =  DateTime.Parse(fila["UltimaActividad"].ToString());
                lista.Add(SensorActivo);
            }

            return lista;
        }

        public SensorActivo Get(int id)
        {
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@Id", id);

            var tabla = db.Consultar("get", sqlParameters);
            foreach (DataRow fila in tabla.Rows)
            {
                var SensorActivo = new SensorActivo();
                SensorActivo.Id = (int) fila["Id"];
                SensorActivo.Nombre = fila["Nombre"].ToString();
                SensorActivo.Url= fila["Url"].ToString();
                SensorActivo.Conectado = (bool) fila["Conectado"];
                SensorActivo.Encendido = (bool) fila["Encendido"];
                SensorActivo.Estado = fila["Estado"].ToString();
                SensorActivo.UltimaActividad =  DateTime.Parse(fila["UltimaActividad"].ToString());
                return SensorActivo;
            }

            return null;
        }

        public void Eliminar(SensorActivo model)
        {
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@Id", model.Id);

            db.Escribir("remove", sqlParameters);
        }




    }
}
