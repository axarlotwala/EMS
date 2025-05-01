using Dapper;
using EMS.Common;
using EMS.DataAccess.IRepository;
using EMS.DbConnection;
using EMS.Model;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace EMS.DataAccess.Repository
{
    public class EmployeeMasterReposiotory : IEmployeeMaster
    {
        private readonly IDbFactory _dbConnect;
       
        public EmployeeMasterReposiotory(IDbFactory connection)
        {
            _dbConnect = connection;
        }

        public async Task<ApiResponse> AddEmployee(EmployeeModel employee)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                string ProcedureName = "InsertEmployee";

                int OutResult = 0;

                var param = new DynamicParameters();
                param.Add("@FirstName", employee.FirstName);
                param.Add("@LastName", employee.LastName);
                param.Add("@Address", employee.Address);
                param.Add("@BloodGroup", employee.BloodGroup);
                param.Add("@JoiningDate", employee.JoiningDate, dbType: DbType.DateTime);
                param.Add("@DateOfBirth", employee.DateOfBirth, dbType: DbType.DateTime);
                param.Add("@Experiance", employee.Experiance);
                param.Add("@Salary", employee.Salary);
                param.Add("@EmployeeCode", employee.EmployeeCode);
                param.Add("@Position", employee.Position);
                param.Add("@Email", employee.Email);
                param.Add("@PhoneNumber", employee.PhoneNumber);
                param.Add("@WhatsappNo", employee.WhatsappNo);
                param.Add("@City", employee.City);
                param.Add("@OutResult", 0, direction: ParameterDirection.Output);

                await SqlMapper.ExecuteAsync(_dbConnect.GetConnection(), ProcedureName, param, commandType: CommandType.StoredProcedure, commandTimeout: 5000);
                OutResult = param.Get<int>("@OutResult");

                if (OutResult == 1)
                {
                    response.Success = 1;
                    response.Message = "Employee Create SuccessFully";
                }
                else
                {
                    response.Success = 0;
                    response.Message = "Something Wrong To Create Employee";
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _dbConnect.CloseConnection();
            }

            return response;
        }

        public async Task<IList<BulkRegisterEmployeeResponse>> BulkRegistrationEmployee(int CreatedID,string filePath)
        {
            try
            {
                ApiResponse apiResponse = new ApiResponse();

                string ProcedureName = "BulkInsertEmployee";
                var param = new DynamicParameters();

                
               

                CommonUtils commonUtils = new CommonUtils();
                var dt = commonUtils.ConvertExcelToDataTable(filePath);
                DataTable datatable = ImportEmployee();

                if (dt.Rows.Count > 0)
                {
                    dt.Columns.Add("SrNo", typeof(int));

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dt.Rows[i]["SrNo"] = i + 1;
                    }

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dataRow = datatable.NewRow();
                        dataRow["SrNo"] = dt.Rows[i]["SrNo"];
                        dataRow["FirstName"] = dt.Rows[i]["FirstName"];
                        dataRow["LastName"] = dt.Rows[i]["LastName"];
                        dataRow["Address"] = dt.Rows[i]["Address"];
                        dataRow["BloodGroup"] = dt.Rows[i]["BloodGroup"];
                        dataRow["JoiningDate"] = dt.Rows[i]["JoiningDate"];
                        dataRow["DateOfBirth"] = dt.Rows[i]["DateOfBirth"];
                        dataRow["Experience"] = dt.Rows[i]["Experience"];
                        dataRow["Salary"] = dt.Rows[i]["Salary"];
                        dataRow["Position"] = dt.Rows[i]["Position"];
                        dataRow["Email"] = dt.Rows[i]["Email"];
                        dataRow["PhoneNumber"] = dt.Rows[i]["PhoneNumber"];
                        dataRow["WhatsappNo"] = dt.Rows[i]["WhatsappNo"];
                        dataRow["City"] = dt.Rows[i]["City"];
                        datatable.Rows.Add(dataRow);
                    }
                }

                param.Add("@CreatedID", CreatedID);
                param.Add("@TblBulkEmployee", datatable.AsTableValuedParameter("TypeTblEmployeeRegister"));

                //param.Add("@OutResult", 0, direction: ParameterDirection.Output);

                var result = await _dbConnect.GetConnection().QueryAsync<BulkRegisterEmployeeResponse>(ProcedureName, param, commandType: CommandType.StoredProcedure);
                return result.ToList();
                //return apiResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _dbConnect.CloseConnection();
            }

          //  return response;
        }

        public async Task<IList<EmployeeGetModel>> GetAllEmployee()
        {
            string proceName = "GetAllEmployee";

            try
            {
                var result = await _dbConnect.GetConnection().QueryAsync<EmployeeGetModel>(proceName, commandType: CommandType.StoredProcedure);
                return result.ToList();
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

        public DataTable ImportEmployee()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SrNo", typeof(int));
            dt.Columns.Add("FirstName", typeof(string));
            dt.Columns.Add("LastName", typeof(string));
            dt.Columns.Add("Address", typeof(string));
            dt.Columns.Add("BloodGroup", typeof(string));
            dt.Columns.Add("JoiningDate", typeof(DateTime));
            dt.Columns.Add("DateOfBirth", typeof(DateTime));
            dt.Columns.Add("Experience", typeof(decimal));
            dt.Columns.Add("Salary", typeof(decimal));
            dt.Columns.Add("Position", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("PhoneNumber", typeof(string));
            dt.Columns.Add("WhatsappNo", typeof(string));
            dt.Columns.Add("City", typeof(string));
            return dt;
        }
        
    }
}
