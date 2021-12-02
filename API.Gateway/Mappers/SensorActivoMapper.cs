using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataAccess;

namespace API.Gateway.Mappers
{
    public class SensorActivoMapper
    {
        private readonly SensorActivoData db;

        public SensorActivoMapper()
        {
            db = SensorActivoData.GetInstance();
        }

        public void Agregar(Modelos.SensorActivo model)
        {
            var sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@Nombre", model.Nombre);
            sqlParameters[1] = new SqlParameter("@Url", model.Url);
            sqlParameters[2] = new SqlParameter("@Conectado", model.Conectado);

            db.Escribir("agregar", sqlParameters);
        }

        public void Actualizar(Modelos.SensorActivo model)
        {
            var sqlParameters = new SqlParameter[5];
            sqlParameters[0] = new SqlParameter("@Id", model.Id);
            sqlParameters[1] = new SqlParameter("@Nombre", model.Nombre);
            sqlParameters[2] = new SqlParameter("@Url", model.Url);
            sqlParameters[3] = new SqlParameter("@Conectado", model.Conectado);
            sqlParameters[4] = new SqlParameter("@Estado", model.Estado);

            db.Escribir("actualizar", sqlParameters);
        }

        public List<Modelos.SensorActivo> Listar()
        {
            
            var lista = new List<Modelos.SensorActivo>();
            var tabla = db.Consultar("getAll", null);
            foreach (DataRow fila in tabla.Rows)
            {
                var SensorActivo = new Modelos.SensorActivo();
                SensorActivo.Id = (int) fila["Id"];
                SensorActivo.Nombre = fila["Nombre"].ToString();
                SensorActivo.Url= fila["Url"].ToString();
                SensorActivo.Conectado = (bool) fila["Conectado"];
                SensorActivo.Estado = fila["Estado"].ToString();
                lista.Add(SensorActivo);
            }

            return lista;
        }

        public Modelos.SensorActivo Get(int id)
        {
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@Id", id);

            var tabla = db.Consultar("get", sqlParameters);
            foreach (DataRow fila in tabla.Rows)
            {
                var sensorActivo = new Modelos.SensorActivo();
                sensorActivo.Id = (int) fila["Id"];
                sensorActivo.Nombre = fila["Nombre"].ToString();
                sensorActivo.Url= fila["Url"].ToString();
                sensorActivo.Conectado = (bool) fila["Conectado"];
                sensorActivo.Estado = fila["Estado"].ToString();
                return sensorActivo;
            }

            return null;
        }

        public void Eliminar(Modelos.SensorActivo model)
        {
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@Id", model.Id);

            db.Escribir("remove", sqlParameters);
        }




    }
}
