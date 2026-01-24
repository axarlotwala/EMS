
using Microsoft.Data.SqlClient;
using System.Data;

namespace EMS.DbConnection
{

    public interface IDbFactory
    {
        IDbConnection GetConnection();

        IDbConnection CloseConnection();
    }


    public class DapperDbConnectionFactory : IDbFactory
    {
        public string _connectionString = null;
       
        public IConfiguration _configuration ;
        public IDbConnection _dbConnection;


        public DapperDbConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("ErpDbConnect");

        }

      //  public IDbConnection CreateConnection() => new SqlConnection(_connectionString);

        public IDbConnection GetConnection()
        {
            //var connection = CreateConnection();

            //if (_dbConnection == null)
            //{
            //    _dbConnection = new SqlConnection(_connectionString);
            //}

            var conn = new SqlConnection(_connectionString);
            conn.Open();
            return conn;

            //if (_dbConnection.State == ConnectionState.Closed ||
            //    _dbConnection.State == ConnectionState.Broken)
            //{
            //    try
            //    {
            //        _dbConnection.Open();
            //    }
            //    catch
            //    {
            //        // recreate the connection if old one is dead
            //        _dbConnection.Dispose();
            //        _dbConnection = new SqlConnection(_connectionString);
            //        _dbConnection.Open();
            //    }
            //}
           // return _dbConnection;
        }

        public IDbConnection CloseConnection()
        {

            var conn = new SqlConnection(_connectionString);
            conn.Close();
            return conn;

            //try
            //{
            //    if (_dbConnection.State == ConnectionState.Open)
            //    {
            //        _dbConnection.Close();
            //    }

            //    return _dbConnection;
            //}catch(Exception ex)
            //{
            //    throw ex;
            //}
        }

    }
}
