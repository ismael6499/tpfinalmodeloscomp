using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Data;

namespace SensorActivo.DataMapper
{
    public class Mapper
    {
        private readonly Datos db;

        public Mapper()
        {
            db = Datos.GetInstance();
        }

        public void Agregar(Models.SensorActivo model)
        {
            var sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@Nombre", model.Nombre);
            sqlParameters[1] = new SqlParameter("@Url", model.Url);
            sqlParameters[2] = new SqlParameter("@Conectado", model.Conectado);

            db.Escribir("agregar", sqlParameters);
        }

        public void Actualizar(Models.SensorActivo model)
        {
            var sqlParameters = new SqlParameter[5];
            sqlParameters[0] = new SqlParameter("@Id", model.Id);
            sqlParameters[1] = new SqlParameter("@Nombre", model.Nombre);
            sqlParameters[2] = new SqlParameter("@Url", model.Url);
            sqlParameters[3] = new SqlParameter("@Conectado", model.Conectado);
            sqlParameters[4] = new SqlParameter("@Estado", model.Estado);

            db.Escribir("actualizar", sqlParameters);
        }

        public List<Models.SensorActivo> Listar()
        {
            
            var lista = new List<Models.SensorActivo>();
            var tabla = db.Consultar("getAll", null);
            foreach (DataRow fila in tabla.Rows)
            {
                var SensorActivo = new Models.SensorActivo();
                SensorActivo.Id = (int) fila["Id"];
                SensorActivo.Nombre = fila["Nombre"].ToString();
                SensorActivo.Url= fila["Url"].ToString();
                SensorActivo.Conectado = (bool) fila["Conectado"];
                SensorActivo.Estado = fila["Estado"].ToString();
                lista.Add(SensorActivo);
            }

            return lista;
        }

        public Models.SensorActivo Get(int id)
        {
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@Id", id);

            var tabla = db.Consultar("get", sqlParameters);
            foreach (DataRow fila in tabla.Rows)
            {
                var sensorActivo = new Models.SensorActivo();
                sensorActivo.Id = (int) fila["Id"];
                sensorActivo.Nombre = fila["Nombre"].ToString();
                sensorActivo.Url= fila["Url"].ToString();
                sensorActivo.Conectado = (bool) fila["Conectado"];
                sensorActivo.Estado = fila["Estado"].ToString();
                return sensorActivo;
            }

            return null;
        }

        public void Eliminar(Models.SensorActivo model)
        {
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@Id", model.Id);

            db.Escribir("remove", sqlParameters);
        }




    }
}
