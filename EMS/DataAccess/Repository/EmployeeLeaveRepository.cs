using Azure.Core;
using Dapper;
using EMS.DataAccess.Interface;
using EMS.DbConnection;
using EMS.Model;

namespace EMS.DataAccess.Repository
{
    public class EmployeeLeaveRepository : IEmployeeLeaveRepository
    {
        private readonly IDbFactory _dbConnect;
        public EmployeeLeaveRepository(IDbFactory dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public async Task<ApiResponse> ApplyLeave(RequestLeave request)
        {
            ApiResponse  response = new ApiResponse ();

            string procName = "EmployeeApplyLeave";

            int OutResult = 0;

            try
            {
                DynamicParameters parms = new DynamicParameters ();
                parms.Add("@EmployeeCode",request.EmployeeCode);
                parms.Add("@LeaveDate",request.LeaveDate);
                parms.Add("@reason", request.reason);
                parms.Add("@OutResult",0,dbType : System.Data.DbType.Int32,direction : System.Data.ParameterDirection.Output);

                await SqlMapper.ExecuteAsync(_dbConnect.GetConnection(),procName,parms,commandType : System.Data.CommandType.StoredProcedure);
                OutResult = parms.Get<int>("@OutResult");

                if (OutResult == 1)
                {
                    response.Success = 1;
                    response.Message = "Successfully Apply";
                }
                else
                {
                    response.Success = 0;
                    response.Message = "Something Went Wrong To Apply";
                }

                return response;
            }
            catch(Exception ex) {
                throw ex;
            }
            finally
            {
                _dbConnect.CloseConnection();
            }
        }

        public async Task<ApiResponse> UpdateLeave(int ID,RequestLeave request)
        {
            ApiResponse response = new ApiResponse();

            string procName = "EmployeeUpdateLeave";

            int OutResult = 0;

            try
            {
                DynamicParameters parms = new DynamicParameters();
                parms.Add("@ID",ID);
                parms.Add("@EmployeeCode", request.EmployeeCode);
                parms.Add("@LeaveDate", request.LeaveDate);
                parms.Add("@reason", request.reason);
                parms.Add("@OutResult", 0, dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

                await SqlMapper.ExecuteAsync(_dbConnect.GetConnection(), procName, parms, commandType: System.Data.CommandType.StoredProcedure);
                OutResult = parms.Get<int>("@OutResult");

                if (OutResult == 1)
                {
                    response.Success = 1;
                    response.Message = "Successfully Update";
                }
                else
                {
                    response.Success = 0;
                    response.Message = "Something Went Wrong To Update";
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

        public async Task<EmployeeLeave> EmployeeLeaveGetByID(int ID)
        {

            string procName = "EmployeeLeaveGetByID";

            try
            {
                DynamicParameters parms = new DynamicParameters();
                parms.Add("@ID", ID);

                var result = await _dbConnect.GetConnection().QueryAsync<EmployeeLeave>(procName, parms, commandType: System.Data.CommandType.StoredProcedure);
                return result.FirstOrDefault();
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

        public async Task<IList<EmployeeLeave>> EmployeeAllLeave(RequestLeave request)
        {
            
            string procName = "ViewEmployeeLeave";

            try
            {
                DynamicParameters parms = new DynamicParameters();
                parms.Add("@EmployeeCode", request.EmployeeCode);

                var result = await _dbConnect.GetConnection().QueryAsync<EmployeeLeave>(procName, parms, commandType: System.Data.CommandType.StoredProcedure);
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

        public async Task<IList<EmployeeLeave>> AdminGetAllLeave()
        {
           
            string procName = "AdminGetAllLeave";

            try
            {
                DynamicParameters parms = new DynamicParameters();
               
                var result = await _dbConnect.GetConnection().QueryAsync<EmployeeLeave>(procName, parms, commandType: System.Data.CommandType.StoredProcedure);
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

        public async Task<ApiResponse> AdminUpdateLeaveStatus(AdminUpdateStatus request)
        {
            ApiResponse response = new ApiResponse();

            string procName = "AdminaUpdateEmployeeLeave";

            int OutResult = 0;

            try
            {
                DynamicParameters parms = new DynamicParameters();
                parms.Add("@ID", request.ID);
                parms.Add("@Status", request.Status);
                parms.Add("@OutResult", 0 , dbType : System.Data.DbType.Int32,direction : System.Data.ParameterDirection.Output);

                await SqlMapper.ExecuteAsync(_dbConnect.GetConnection(),procName,parms,commandType : System.Data.CommandType.StoredProcedure);
                OutResult = parms.Get<int>("@OutResult");

                if (OutResult == 1)
                {
                    response.Success = 1;
                    response.Message = "Successfully Update";
                }
                else
                {
                    response.Success = 0;
                    response.Message = "Something Went Wrong To Update";
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
