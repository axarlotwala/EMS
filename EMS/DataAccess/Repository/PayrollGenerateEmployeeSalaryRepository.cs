using Dapper;
using EMS.DataAccess.Interface;
using EMS.DbConnection;
using EMS.Model;

namespace EMS.DataAccess.Repository
{
    public class PayrollGenerateEmployeeSalaryRepository : IPayrollGenerateEmployeeSalaryRepository
    {
        private readonly IDbFactory _dbConnect;

        public PayrollGenerateEmployeeSalaryRepository(IDbFactory connection)
        {
            _dbConnect= connection;
        }


        public async Task<PayrollGeneratedEmployeeSalary> PayrollGeneratedEmployeeSalary(RequestPayrollSalary request)
        {
            string procName = "PayrollEmployeeSalaryGenerate";

            try
            {

                DynamicParameters parms = new DynamicParameters();
                parms.Add("@EmployeeCode", request.EmployeeCode);
                parms.Add("@Year", request.Year);
                parms.Add("@Month", request.Month);

                var result = await _dbConnect.GetConnection().QueryAsync<PayrollGeneratedEmployeeSalary>(procName, parms, commandType: System.Data.CommandType.StoredProcedure);
                return result.FirstOrDefault();

            }catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                _dbConnect.CloseConnection();
            }
        }

        public async Task<IList<PayrollGeneratedEmployeeSalary>> GetPayrollAllEmployeeSalary(RequestPayrollSalary request)
        {
            string procName = "GetPayrollAllEmployeeSalary";

            try
            {

                DynamicParameters parms = new DynamicParameters();
                parms.Add("@Year", request.Year);
                parms.Add("@Month", request.Month);
                parms.Add("@EmployeeCode", request.EmployeeCode);

                var result = await _dbConnect.GetConnection().QueryAsync<PayrollGeneratedEmployeeSalary>(procName, parms, commandType: System.Data.CommandType.StoredProcedure);
                return result.ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _dbConnect.CloseConnection();
            }
        }
    }
}
