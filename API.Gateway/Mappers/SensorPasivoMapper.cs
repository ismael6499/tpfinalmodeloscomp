using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataAccess;

namespace API.Gateway.Mappers
{
    public class SensorPasivoMapper
    {
        private readonly SensorPasivoData db;

        public SensorPasivoMapper()
        {
            db = SensorPasivoData.GetInstance();
        }

        public void Agregar(Modelos.SensorPasivo model)
        {
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@Nombre", model.Nombre);
            sqlParameters[1] = new SqlParameter("@Url", model.Url);

            db.Escribir("agregar", sqlParameters);
        }

        public void Actualizar(Modelos.SensorPasivo model)
        {
            var sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@Id", model.Id);
            sqlParameters[1] = new SqlParameter("@Nombre", model.Nombre);
            sqlParameters[2] = new SqlParameter("@Url", model.Url);
            sqlParameters[3] = new SqlParameter("@Libre", model.Libre);

            db.Escribir("actualizar", sqlParameters);
        }

        public List<Modelos.SensorPasivo> Listar()
        {
            
            var lista = new List<Modelos.SensorPasivo>();
            var tabla = db.Consultar("getAll", null);
            foreach (DataRow fila in tabla.Rows)
            {
                var sensorPasivo = new Modelos.SensorPasivo();
                sensorPasivo.Id = (int) fila["Id"];
                sensorPasivo.Nombre = fila["Nombre"].ToString();
                sensorPasivo.Url= fila["Url"].ToString();
                sensorPasivo.Libre = (bool) fila["Libre"];
                lista.Add(sensorPasivo);
            }

            return lista;
        }

        public Modelos.SensorPasivo Get(int id)
        {
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@Id", id);

            var tabla = db.Consultar("get", sqlParameters);
            foreach (DataRow fila in tabla.Rows)
            {
                var sensorPasivo = new Modelos.SensorPasivo();
                sensorPasivo.Id = (int) fila["Id"];
                sensorPasivo.Nombre = fila["Nombre"].ToString();
                sensorPasivo.Url= fila["Url"].ToString();
                sensorPasivo.Libre = (bool) fila["Libre"];
                return sensorPasivo;
            }

            return null;
        }

        public void Eliminar(Modelos.SensorPasivo model)
        {
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@Id", model.Id);

            db.Escribir("remove", sqlParameters);
        }

    }
}
