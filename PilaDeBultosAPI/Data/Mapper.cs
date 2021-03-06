using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Data;
using PilaDeBultosAPI.Modelos;

namespace PilaDeBultosAPI.Data
{
    public class Mapper
    {
        private readonly Datos db;

        public Mapper()
        {
            db = Datos.GetInstance();
        }

        public void Agregar(PilaDeBultos model)
        {
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@Nombre", model.Nombre);
            sqlParameters[1] = new SqlParameter("@Url", model.Url);

            db.Escribir("agregar", sqlParameters);
        }

        public void Actualizar(PilaDeBultos model)
        {
            var sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@Id", model.Id);
            sqlParameters[1] = new SqlParameter("@Nombre", model.Nombre);
            sqlParameters[2] = new SqlParameter("@Url", model.Url);

            db.Escribir("actualizar", sqlParameters);
        }

        public List<PilaDeBultos> Listar()
        {
            
            var lista = new List<PilaDeBultos>();
            var tabla = db.Consultar("getAll", null);
            foreach (DataRow fila in tabla.Rows)
            {
                var PilaDeBultos = new PilaDeBultos();
                PilaDeBultos.Id = (int) fila["Id"];
                PilaDeBultos.Nombre = fila["Nombre"].ToString();
                PilaDeBultos.Url= fila["Url"].ToString();
                lista.Add(PilaDeBultos);
            }

            return lista;
        }

        public PilaDeBultos Get(int id)
        {
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@Id", id);

            var tabla = db.Consultar("get", sqlParameters);
            foreach (DataRow fila in tabla.Rows)
            {
                var PilaDeBultos = new PilaDeBultos();
                PilaDeBultos.Id = (int) fila["Id"];
                PilaDeBultos.Nombre = fila["Nombre"].ToString();
                PilaDeBultos.Url= fila["Url"].ToString();
                return PilaDeBultos;
            }

            return null;
        }

        public void Eliminar(PilaDeBultos model)
        {
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@Id", model.Id);

            db.Escribir("remove", sqlParameters);
        }




    }
}
