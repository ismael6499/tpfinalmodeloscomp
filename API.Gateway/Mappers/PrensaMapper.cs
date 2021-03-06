using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using API.Gateway.Modelos;
using DataAccess;

namespace API.Gateway.Mappers
{
    public class PrensaMapper
    {
        private readonly PrensaData db;

        public PrensaMapper()
        {
            db = PrensaData.GetInstance();
        }

        public void Agregar(Prensa model)
        {
            var sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@Nombre", model.Nombre);
            sqlParameters[1] = new SqlParameter("@Url", model.Url);
            sqlParameters[2] = new SqlParameter("@Encendido", model.Encendido);

            db.Escribir("agregar", sqlParameters);
        }

        public void Actualizar(Prensa model)
        {
            var sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@Id", model.Id);
            sqlParameters[1] = new SqlParameter("@Nombre", model.Nombre);
            sqlParameters[2] = new SqlParameter("@Url", model.Url);
            sqlParameters[3] = new SqlParameter("@Encendido", model.Encendido);
            
            db.Escribir("actualizar", sqlParameters);
        }

        public List<Prensa> Listar()
        {
            
            var lista = new List<Prensa>();
            var tabla = db.Consultar("getAll", null);
            foreach (DataRow fila in tabla.Rows)
            {
                var Prensa = new Prensa();
                Prensa.Id = (int) fila["Id"];
                Prensa.Nombre = fila["Nombre"].ToString();
                Prensa.Url= fila["Url"].ToString();
                Prensa.Encendido = (bool)fila["Encendido"];
                lista.Add(Prensa);
            }

            return lista;
        }

        public Prensa Get(int id)
        {
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@Id", id);

            var tabla = db.Consultar("get", sqlParameters);
            foreach (DataRow fila in tabla.Rows)
            {
                var Prensa = new Prensa();
                Prensa.Id = (int) fila["Id"];
                Prensa.Nombre = fila["Nombre"].ToString();
                Prensa.Url= fila["Url"].ToString();
                Prensa.Encendido = (bool)fila["Encendido"];
                return Prensa;
            }

            return null;
        }

        public void Eliminar(Prensa model)
        {
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@Id", model.Id);

            db.Escribir("remove", sqlParameters);
        }




    }
}
