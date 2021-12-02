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

        public void ChangeEstado(Models.SensorActivo model)
        {
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@Url", model.Url);
            sqlParameters[1] = new SqlParameter("@Estado", model.Estado);

            db.Escribir("changeestado", sqlParameters);
        }
        



    }
}
