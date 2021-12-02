using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Data;

namespace Sensor.Pasivo.Data
{
    public class Mapper
    {
        private readonly Datos db;

        public Mapper()
        {
            db = Datos.GetInstance();
        }

        public void ChangeEstado(Models.SensorPasivo model)
        {
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@Url", model.Url);
            sqlParameters[1] = new SqlParameter("@Libre", model.Libre);

            db.Escribir("changeestado", sqlParameters);
        }

        
        public void Agregar(Models.SensorPasivo model)
        {
            var sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@Nombre", model.Nombre);
            sqlParameters[1] = new SqlParameter("@Url", model.Url);

            db.Escribir("agregar", sqlParameters);
        }

        public void Actualizar(Models.SensorPasivo model)
        {
            var sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@Id", model.Id);
            sqlParameters[1] = new SqlParameter("@Nombre", model.Nombre);
            sqlParameters[2] = new SqlParameter("@Url", model.Url);
            sqlParameters[3] = new SqlParameter("@Libre", model.Libre);

            db.Escribir("actualizar", sqlParameters);
        }

        public List<Models.SensorPasivo> Listar()
        {
            
            var lista = new List<Models.SensorPasivo>();
            var tabla = db.Consultar("getAll", null);
            foreach (DataRow fila in tabla.Rows)
            {
                var sensorPasivo = new Models.SensorPasivo();
                sensorPasivo.Id = (int) fila["Id"];
                sensorPasivo.Nombre = fila["Nombre"].ToString();
                sensorPasivo.Url= fila["Url"].ToString();
                sensorPasivo.Libre = (bool) fila["Libre"];
                lista.Add(sensorPasivo);
            }

            return lista;
        }

        public Models.SensorPasivo Get(int id)
        {
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@Id", id);

            var tabla = db.Consultar("get", sqlParameters);
            foreach (DataRow fila in tabla.Rows)
            {
                var sensorPasivo = new Models.SensorPasivo();
                sensorPasivo.Id = (int) fila["Id"];
                sensorPasivo.Nombre = fila["Nombre"].ToString();
                sensorPasivo.Url= fila["Url"].ToString();
                sensorPasivo.Libre = (bool) fila["Libre"];
                return sensorPasivo;
            }

            return null;
        }

        public void Eliminar(Models.SensorPasivo model)
        {
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@Id", model.Id);

            db.Escribir("remove", sqlParameters);
        }

    }
}
