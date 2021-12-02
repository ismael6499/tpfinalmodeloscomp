using System;
using System.Data;
using System.Data.SqlClient;
using CintaAPI;
using CintaAPI.Data;

namespace Data
{
    public class Datos
    {
        private static readonly object _lock = new object();
        private static Datos _instance;

        private readonly SqlConnection _connection;
        private SqlTransaction transaction;

        private Datos()
        {
            var connectionString = Program.configuration["ConnectionStrings:ConnectionString"];
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

        public DataTable Consultar(string consulta, SqlParameter[] sqlparameters)
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

        public static Datos GetInstance()
        {
            lock (_lock)
            {
                if (_instance == null) _instance = new Datos();
                return _instance;
            }
        }
    }
}