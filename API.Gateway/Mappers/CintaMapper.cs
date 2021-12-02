using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using API.Gateway.Modelos;
using DataAccess;

namespace API.Gateway.Mappers
{
    public class CintaMapper
    {
        private readonly CintaData db;

        public CintaMapper()
        {
            db = CintaData.GetInstance();
        }

        public void Agregar(Cinta model)
        {
            var sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@Nombre", model.Nombre);
            sqlParameters[1] = new SqlParameter("@Url", model.Url);
            sqlParameters[2] = new SqlParameter("@Conectado", model.Conectado);
            sqlParameters[3] = new SqlParameter("@Encendido", model.Encendido);

            db.Escribir("agregar", sqlParameters);
        }

        public void Actualizar(Cinta model)
        {
            var sqlParameters = new SqlParameter[5];
            sqlParameters[0] = new SqlParameter("@Id", model.Id);
            sqlParameters[1] = new SqlParameter("@Nombre", model.Nombre);
            sqlParameters[2] = new SqlParameter("@Url", model.Url);
            sqlParameters[3] = new SqlParameter("@Conectado", model.Conectado);
            sqlParameters[4] = new SqlParameter("@Encendido", model.Encendido);

            db.Escribir("actualizar", sqlParameters);
        }

        public List<Cinta> Listar()
        {
            
            var lista = new List<Cinta>();
            var tabla = db.Consultar("getAll", null);
            foreach (DataRow fila in tabla.Rows)
            {
                var Cinta = new Cinta();
                Cinta.Id = (int) fila["Id"];
                Cinta.Nombre = fila["Nombre"].ToString();
                Cinta.Url= fila["Url"].ToString();
                Cinta.Conectado = (bool) fila["Conectado"];
                Cinta.Encendido = (bool) fila["Encendido"];
                lista.Add(Cinta);
            }

            return lista;
        }

        public Cinta Get(int id)
        {
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@Id", id);

            var tabla = db.Consultar("get", sqlParameters);
            foreach (DataRow fila in tabla.Rows)
            {
                var Cinta = new Cinta();
                Cinta.Id = (int) fila["Id"];
                Cinta.Nombre = fila["Nombre"].ToString();
                Cinta.Url= fila["Url"].ToString();
                Cinta.Conectado = (bool) fila["Conectado"];
                Cinta.Encendido = (bool) fila["Encendido"];
                return Cinta;
            }

            return null;
        }

        public void Eliminar(Cinta model)
        {
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@Id", model.Id);

            db.Escribir("remove", sqlParameters);
        }




    }
}
