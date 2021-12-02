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
    public class PilaDeBultosMapper
    {
        private readonly PilaDeBultosData db;

        public PilaDeBultosMapper()
        {
            db = PilaDeBultosData.GetInstance();
        }

        public void Agregar(PilaDeBultos model)
        {
            var sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@Nombre", model.Nombre);
            sqlParameters[1] = new SqlParameter("@Url", model.Url);
            sqlParameters[2] = new SqlParameter("@Conectado", model.Conectado);
            sqlParameters[3] = new SqlParameter("@Encendido", model.Encendido);

            db.Escribir("agregar", sqlParameters);
        }

        public void Actualizar(PilaDeBultos model)
        {
            var sqlParameters = new SqlParameter[6];
            sqlParameters[0] = new SqlParameter("@Id", model.Id);
            sqlParameters[1] = new SqlParameter("@Nombre", model.Nombre);
            sqlParameters[2] = new SqlParameter("@Url", model.Url);
            sqlParameters[3] = new SqlParameter("@Conectado", model.Conectado);
            sqlParameters[4] = new SqlParameter("@Encendido", model.Encendido);
            sqlParameters[5] = new SqlParameter("@UltimaActividad", model.UltimaActividad);

            db.Escribir("agregar", sqlParameters);
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
                PilaDeBultos.Encendido = (bool) fila["Encendido"];
                PilaDeBultos.UltimaActividad =  DateTime.Parse(fila["UltimaActividad"].ToString());
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
                PilaDeBultos.Encendido = (bool) fila["Encendido"];
                PilaDeBultos.UltimaActividad =  DateTime.Parse(fila["UltimaActividad"].ToString());
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
