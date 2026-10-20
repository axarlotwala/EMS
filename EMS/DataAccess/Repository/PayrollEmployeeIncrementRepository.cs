using Dapper;
using EMS.DataAccess.Interface;
using EMS.DbConnection;
using EMS.Model;

namespace EMS.DataAccess.Repository
{
    public class PayrollEmployeeIncrementRepository : IPayrollEmployeeIncrementRepository
    {

        private readonly IDbFactory _dbConnect;

        public PayrollEmployeeIncrementRepository(IDbFactory dbFactory)
        {
            _dbConnect = dbFactory;
        }

        public async Task<GetEmployeeWiseFixSalary> EmployeeWiseGetFixSalary(RequestEmployeeFixSalary request)
        {
            string procName = "EmployeeWiseGetFixSalary";

            try
            {
                DynamicParameters parms = new DynamicParameters();
                parms.Add("@EmployeeCode", request.EmployeeCode);

                var result = await _dbConnect.GetConnection().QueryAsync<GetEmployeeWiseFixSalary>(procName, parms,commandType : System.Data.CommandType.StoredProcedure);
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

        public async Task<ApiResponse> InsertPayrollEmployeeIncrement(RequestEmployeeIncrement request)
        {
            ApiResponse response = new ApiResponse();

            string procName = "InsertPayrollEmployeeIncrement";

            int OutResult = 0;

            try
            {

                DynamicParameters parms = new DynamicParameters();
                parms.Add("@EmployeeID", request.EmployeeID);
                parms.Add("@EmployeeCode", request.EmployeeCode);
                parms.Add("@FixSalary", request.FixSalary);
                parms.Add("@IncrementPercentage", request.IncrementPercentage);
                parms.Add("@IncrementAmount", request.IncrementAmount);
                parms.Add("@FinalFixSalary", request.FinalFixSalary);
                parms.Add("@EffectiveFromDate", request.EffectiveFromDate);
                parms.Add("@OutResult", 0 , dbType : System.Data.DbType.Int32,direction : System.Data.ParameterDirection.Output);

                await SqlMapper.ExecuteAsync(_dbConnect.GetConnection(), procName, parms, commandType: System.Data.CommandType.StoredProcedure);
                OutResult = parms.Get<int>("@OutResult");

                if (OutResult == 1)
                {
                    response.Success = 1;
                    response.Message = "Successfully Increment Employee Salary";
                }
                else
                {
                    response.Success = 0;
                    response.Message = "Something Went Wrong To Increment";
                }


                return response;

            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                _dbConnect.CloseConnection();
            }
        }

        public async Task<ApiResponse> UpdatePayrollEmployeeIncrement(int ID,RequestEmployeeIncrement request)
        {
            ApiResponse response = new ApiResponse();

            string procName = "UpdatePayrollEmployeeIncrement";

            int OutResult = 0;

            try
            {

                DynamicParameters parms = new DynamicParameters();
                parms.Add("@ID", ID);
                parms.Add("@EmployeeID", request.EmployeeID);
                parms.Add("@EmployeeCode", request.EmployeeCode);
                parms.Add("@FixSalary", request.FixSalary);
                parms.Add("@IncrementPercentage", request.IncrementPercentage);
                parms.Add("@IncrementAmount", request.IncrementAmount);
                parms.Add("@FinalFixSalary", request.FinalFixSalary);
                parms.Add("@EffectiveFromDate", request.EffectiveFromDate);
                parms.Add("@OutResult", 0, dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

                await SqlMapper.ExecuteAsync(_dbConnect.GetConnection(), procName, parms, commandType: System.Data.CommandType.StoredProcedure);
                OutResult = parms.Get<int>("@OutResult");

                if (OutResult == 1)
                {
                    response.Success = 1;
                    response.Message = "Update SuccessFully";
                }
                else
                {
                    response.Success = 0;
                    response.Message = "Something Went Wrong To Increment";
                }


                return response;

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
