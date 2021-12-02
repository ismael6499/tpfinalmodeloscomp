using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;

namespace BrazoAPI.Data
{
    public class Logger
    {
        
        private static readonly object _lock = new object();
        private static Logger _instance;

        private readonly SqlConnection _connection;
        private SqlTransaction transaction;

        private string componente = "Brazo";
        public Logger()
        {
            var connectionString = Program.configuration["ConnectionStrings:ConnectionMicroservicios"];
            _connection = new SqlConnection(connectionString);
        }
        
        public void OpenConnection()
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

        public void CloseConnection()
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

        public void Escribir(string consulta, SqlParameter[] sqlparameters)
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
    }
}