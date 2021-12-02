using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using API.Gateway.Modelos;
using DataAccess;

namespace API.Gateway.Mappers
{
    public class BrazoMapper
    {
        private readonly BrazoData db;

        public BrazoMapper()
        {
            db = BrazoData.GetInstance();
        }

        public void Agregar(Brazo model)
        {
            var sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@Nombre", model.Nombre);
            sqlParameters[1] = new SqlParameter("@Url", model.Url);
            sqlParameters[2] = new SqlParameter("@Encendido", model.Encendido);

            db.Escribir("agregar", sqlParameters);
        }

        public void Actualizar(Brazo model)
        {
            var sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@Id", model.Id);
            sqlParameters[1] = new SqlParameter("@Nombre", model.Nombre);
            sqlParameters[2] = new SqlParameter("@Url", model.Url);
            sqlParameters[3] = new SqlParameter("@Encendido", model.Encendido);

            db.Escribir("actualizar", sqlParameters);
        }

        public List<Brazo> Listar()
        {
            
            var lista = new List<Brazo>();
            var tabla = db.Consultar("getAll", null);
            foreach (DataRow fila in tabla.Rows)
            {
                var brazo = new Brazo();
                brazo.Id = (int) fila["Id"];
                brazo.Nombre = fila["Nombre"].ToString();
                brazo.Url= fila["Url"].ToString();
                brazo.Encendido = (bool) fila["Encendido"];
                lista.Add(brazo);
            }

            return lista;
        }

        public Brazo Get(int id)
        {
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@Id", id);

            var tabla = db.Consultar("get", sqlParameters);
            foreach (DataRow fila in tabla.Rows)
            {
                var brazo = new Brazo();
                brazo.Id = (int) fila["Id"];
                brazo.Nombre = fila["Nombre"].ToString();
                brazo.Url= fila["Url"].ToString();
                brazo.Encendido = (bool) fila["Encendido"];
                return brazo;
            }

            return null;
        }

        public void Eliminar(Brazo model)
        {
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@Id", model.Id);

            db.Escribir("remove", sqlParameters);
        }

    }
}
