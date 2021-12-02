using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using Data;
using WebAppGUI;
using WebAppGUI.Modelos;

namespace WebAppGUI.Data
{
    public class Logger
    {
        
        private static readonly object _lock = new object();
        private static Logger _instance;

        private readonly SqlConnection _connection;
        private SqlTransaction transaction;
        
        private string componente = "Web UI";
        public Logger()
        {
            var connectionString = Program.configuration["ConnectionStrings:ConnectionString"];
            _connection = new SqlConnection(connectionString);
        }
        
        private void OpenConnection()
        {
            try
            {
                if (_connection.State == ConnectionState.Open) return;
                _connection.Open();
            }
            catch (Exception e)
            {
                Logger.GetInstance().WriteLogError(e);
                throw;
            }
        }

        private void CloseConnection()
        {
            try
            {
                if (_connection.State == ConnectionState.Closed) return;
                _connection.Close();
            }
            catch (Exception e)
            {
                Logger.GetInstance().WriteLogError(e);
                throw;
            }
        }

        private DataTable Consultar(string consulta, SqlParameter[] sqlparameters)
        {
            OpenConnection();
            var tabla = new DataTable();
            var command = new SqlCommand();
            command.Connection = _connection;
            command.CommandText = consulta;
            var adaptador = new SqlDataAdapter(command);
            adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (sqlparameters != null && sqlparameters.Length > 0)
                adaptador.SelectCommand.Parameters.AddRange(sqlparameters);

            try
            {
                adaptador.Fill(tabla);
                CloseConnection();
                return tabla;
            }
            catch (Exception ex)
            {
                Logger.GetInstance().WriteLogError(ex.Message);
                CloseConnection();
                throw;
            }
        }
        
        private void Escribir(string consulta, SqlParameter[] sqlparameters)
        {
            try
            {
                OpenConnection();
                transaction = _connection.BeginTransaction();
                var cmd = new SqlCommand();
                cmd.Connection = _connection;
                cmd.CommandText = consulta;
                cmd.Transaction = transaction;
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    cmd.Parameters.AddRange(sqlparameters);
                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }

                CloseConnection();
            }
            catch (Exception e)
            {
                Logger.GetInstance().WriteLogError(e);
                throw;
            }
        }

        public static Logger GetInstance()
        {
            lock (_lock)
            {
                if (_instance == null) _instance = new Logger();
                return _instance;
            }
        }

        public List<Log> ReadLogs()
        {
            List<Log> lista = new List<Log>();
            DataTable dataTable = Consultar("readLogs",null);
            foreach (DataRow row in dataTable.Rows)
            {
                var log = new Log();
                
                int id = int.Parse(row["id_log"].ToString()!);
                log.Id = id;

                log.Componente = row["componente"].ToString();

                log.Descripcion = row["descripcion"].ToString();

                log.Fecha = DateTime.Parse(row["fecha"].ToString()!);

                log.Tipo = row["tipo"].ToString();
                lista.Add(log);
            }

            return lista;
        }
        

        public void WriteLog(string description)
        {
            Console.WriteLine(description);

            SqlParameter[] sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@componente", componente);
            sqlParameters[1] = new SqlParameter("@descripcion", description);
            sqlParameters[2] = new SqlParameter("@tipo", "Debug");
              Thread.Sleep(300);
Escribir("writeLog",sqlParameters);
        }
        
         public void WriteLogError(string description)
        {
            Console.WriteLine(description);

            SqlParameter[] sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@componente", componente);
            sqlParameters[1] = new SqlParameter("@descripcion", description);
            sqlParameters[2] = new SqlParameter("@tipo", "Error");
              Thread.Sleep(300);
Escribir("writeLog",sqlParameters);
        }
         
         public void WriteLogError(Exception exception)
         {
             var msjError = exception.ToString();
             Console.WriteLine(msjError);

             SqlParameter[] sqlParameters = new SqlParameter[3];
             sqlParameters[0] = new SqlParameter("@componente", componente);
             sqlParameters[1] = new SqlParameter("@descripcion", msjError);
             sqlParameters[2] = new SqlParameter("@tipo", "Error");
             Thread.Sleep(300);
             Escribir("writeLog", sqlParameters);
         }

         public BultoLog GetBultoLog()
         {
             DataTable dataTable = Consultar("consultarDataBultos",null);
             foreach (DataRow row in dataTable.Rows)
             {
                 var bultoLog = new BultoLog();
                 bultoLog.Ingresados = int.Parse(row["ingresados"].ToString());
                 bultoLog.Prensado = int.Parse(row["prensando"].ToString());
                 bultoLog.Apilados = int.Parse(row["apilados"].ToString());
                 return bultoLog;
             }

             return new BultoLog();
         }
    }

    public class BultoLog
    {
        public int Ingresados { get; set; }
        public int Prensado { get; set; }
        public int Apilados { get; set; }
    }
}