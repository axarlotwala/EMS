
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
        public IDbConnection _dbConnection;
        public IConfiguration _configuration ;
       
        

        public DapperDbConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("ErpDbConnect");

        }

      //  public IDbConnection CreateConnection() => new SqlConnection(_connectionString);

        public IDbConnection GetConnection()
        {
            //var connection = CreateConnection();

            if (_dbConnection == null)
            {
                _dbConnection = new SqlConnection(_connectionString);
            }

            if (_dbConnection.State == ConnectionState.Closed)
            {
                _dbConnection.Open();
            }
            return _dbConnection;
        }

        public IDbConnection CloseConnection()
        {
            try
            {
                if (_dbConnection.State == ConnectionState.Open)
                {
                    _dbConnection.Close();
                }

                return _dbConnection;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}
