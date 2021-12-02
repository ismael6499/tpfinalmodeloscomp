using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using API.Gateway.Modelos;
using DataAccess;

namespace API.Gateway.Mappers
{
    public class PilaDeBultosMapper
    {
        private readonly PilaDeBultosData db;

        public PilaDeBultosMapper()
        {
            db = PilaDeBultosData.GetInstance();
        }

        public void Agregar(PilaDeBultos model)
        {
            var sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@Nombre", model.Nombre);
            sqlParameters[1] = new SqlParameter("@Url", model.Url);
            sqlParameters[2] = new SqlParameter("@Conectado", model.Conectado);

            db.Escribir("agregar", sqlParameters);
        }

        public void Actualizar(PilaDeBultos model)
        {
            var sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@Id", model.Id);
            sqlParameters[1] = new SqlParameter("@Nombre", model.Nombre);
            sqlParameters[2] = new SqlParameter("@Url", model.Url);
            sqlParameters[3] = new SqlParameter("@Conectado", model.Conectado);

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
                PilaDeBultos.Conectado = (bool) fila["Conectado"];
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
                PilaDeBultos.Conectado = (bool) fila["Conectado"];
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
