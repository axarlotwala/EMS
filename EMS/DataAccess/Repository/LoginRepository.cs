using Dapper;
using EMS.DataAccess.Interface;
using EMS.DbConnection;
using EMS.Model;

namespace EMS.DataAccess.Repository
{
    public class LoginRepository : ILoginRepository
    {
        IDbFactory _dbconnection;
        
        public LoginRepository(IDbFactory dbFactory)
        {
            _dbconnection = dbFactory;   
        }

        public async Task<LoginUser> UserLogin(RequestLogin request)
        {
            try
            {
                string procName = "UserLogin";

                DynamicParameters parm = new DynamicParameters();
                parm.Add("@UserName", request.UserName);
                parm.Add("@Password", request.Password);

                var result = await _dbconnection.GetConnection().QueryAsync<LoginUser>(procName, parm, commandType: System.Data.CommandType.StoredProcedure);
                return result.FirstOrDefault();

            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                _dbconnection.CloseConnection();
            }
        }
    }
}
