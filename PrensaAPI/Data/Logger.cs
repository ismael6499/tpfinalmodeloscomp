using System;
using System.Data;
using System.Data.SqlClient;
using PrensaAPI.Modelos;

namespace PrensaAPI.Data
{
    public class Logger
    {
        private static readonly object _lock = new object();
        private static Logger _instance;

        private readonly SqlConnection _connection;
        private SqlTransaction transaction;

        private string componente = "Prensa";
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
                GetInstance().WriteLogError(e);
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
                GetInstance().WriteLogError(e);
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
                GetInstance().WriteLogError(e);
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
            Escribir("writeLog", sqlParameters);
        }
        
        public void WriteLog(string component, string description)
        {
            Console.WriteLine(description);

            SqlParameter[] sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@componente", component);
            sqlParameters[1] = new SqlParameter("@descripcion", description);
            sqlParameters[2] = new SqlParameter("@tipo", "Debug");
            Escribir("writeLog", sqlParameters);
        }

        public void WriteLogError(string description)
        {
            Console.WriteLine(description);

            SqlParameter[] sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@componente", componente);
            sqlParameters[1] = new SqlParameter("@descripcion", description);
            sqlParameters[2] = new SqlParameter("@tipo", "Error");
            Escribir("writeLog", sqlParameters);
        }
        
        public void WriteLogError(string component, string description)
        {
            Console.WriteLine(description);

            SqlParameter[] sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@componente", component);
            sqlParameters[1] = new SqlParameter("@descripcion", description);
            sqlParameters[2] = new SqlParameter("@tipo", "Error");
            Escribir("writeLog", sqlParameters);
        }

        public void WriteLogError(Exception exception)
        {
            var msjError = exception.ToString();
            Console.WriteLine(msjError);

            SqlParameter[] sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@componente", componente);
            sqlParameters[1] = new SqlParameter("@descripcion", msjError);
            sqlParameters[2] = new SqlParameter("@tipo", "Error");
            Escribir("writeLog", sqlParameters);
        }
        
        public void SaveBultoLog(Bulto bulto)
        {
            SqlParameter[] sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@globalid", bulto.GlobalId);
            sqlParameters[1] = new SqlParameter("@descripcion", bulto.Descripcion);
            sqlParameters[2] = new SqlParameter("@fecha", bulto.Fecha);
            sqlParameters[3] = new SqlParameter("@estado", bulto.Estado);
            Escribir("saveBultoLog", sqlParameters);
        }
    }
}