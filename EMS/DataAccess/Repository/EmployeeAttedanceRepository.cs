using Dapper;
using EMS.Common;
using EMS.DataAccess.Interface;
using EMS.DbConnection;
using EMS.Model.Employee;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.Tokens;
using System.Data;

namespace EMS.DataAccess.Repository
{
    public class EmployeeAttedanceRepository : IEmployeeAttedanceRepository
    {
        private IDbFactory _dbConnect;
        public EmployeeAttedanceRepository(IDbFactory dbFactory)
        {
            _dbConnect = dbFactory;
        }


        public async Task<BulkUploadAttedanceResponse> GenerateEmployeeAttedance(RequestGenerateEmployeeAttedance request, string filePath)
        {
            string procName = "PayrollGenerateEmloyeeAttedance";

            try
            {
                int OutResult = 0;
                BulkUploadAttedanceResponse response = new BulkUploadAttedanceResponse();
                
                var param = new DynamicParameters();

                string EmployeeCode;
                DateTime attedenceDate;
               


                CommonUtils commonUtils = new CommonUtils();
                var dt = commonUtils.ConvertExcelToDataTable(filePath);
                DataTable datatable = ImportEmployeeAttedance();


                

                if (dt.Rows.Count > 0)
                {
                    DateTime InTime = DateTime.Parse("1900-01-01 00:00:00.000");
                    DateTime OutTime = DateTime.Parse("1900-01-01 00:00:00.000");
                    dt.Columns.Add("SrNo", typeof(int));

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //InTime = DateTime.FromOADate(Convert.ToDouble(dt.Rows[i]["In Time"]));
                        //OutTime = DateTime.FromOADate(Convert.ToDouble(dt.Rows[i]["Out Time"]));
                        dt.Rows[i]["SrNo"] = i + 1;
                        EmployeeCode =  dt.Rows[i]["Employee Code"].ToString();
                        attedenceDate = DateTime.Parse(dt.Rows[i]["Attedance Date"].ToString());

                        if (!EmployeeCode.Equals(request.EmployeeCode))
                        {
                            response.requireField.Add(new AttedanceValidation { ColumnName = $"{"You have Enter Wrong EmployeeCode : "} {EmployeeCode}", Message = "You have Not Select Valid Employee", Success = 0 });
                        }

                        if (dt.Rows[i]["In Time"].ToString() == "" || dt.Rows[i]["In Time"].ToString() == null)
                        {
                            InTime = DateTime.Parse("1900-01-01 00:00:00.000");
                        }
                        else
                        {
                            InTime = DateTime.Parse(dt.Rows[i]["In Time"].ToString());
                        }

                        if (dt.Rows[i]["Out Time"].ToString() == "" || dt.Rows[i]["Out Time"].ToString() == null)
                        {
                            OutTime = DateTime.Parse("1900-01-01 00:00:00.000");
                        }
                        else
                        {
                            OutTime = DateTime.Parse(dt.Rows[i]["Out Time"].ToString());
                        }

                       

                        if (attedenceDate.Year != request.Year || attedenceDate.Month != request.Month)
                        {
                            response.requireField.Add(new AttedanceValidation { ColumnName = "Attedence Date", Message = $"{"Upload Wrong Year Month AttedenceDate : "} {attedenceDate.Year} {attedenceDate.Month}", Date = attedenceDate, Success = 0 });
                        }

                    

                    }




                    if (response.requireField.Count == 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            //var valInTime = dt.Rows[i]["In Time"].ToString();
                            //var valOutTime = dt.Rows[i]["Out Time"].ToString();

                            ////DateTime OutTime = DateTime.Parse("1900-01-01 00:00:00.000");

                            //if (valInTime.IsNullOrEmpty() || valInTime == "")
                            //{
                            //    InTime = DateTime.Parse("1900-01-01 00:00:00.000");
                            //}
                            //else
                            //{
                            //    InTime = DateTime.FromOADate(Convert.ToDouble(dt.Rows[i]["In Time"]));
                            //}

                            //if (valOutTime.IsNullOrEmpty() || valOutTime == "")
                            //{
                            //    OutTime = DateTime.Parse("1900-01-01 00:00:00.000");
                            //}
                            //else
                            //{
                            //    OutTime = DateTime.FromOADate(Convert.ToDouble(dt.Rows[i]["Out Time"]));
                            //}


                            if (dt.Rows[i]["In Time"].ToString() == "" || dt.Rows[i]["In Time"].ToString() == null)
                            {
                                InTime = DateTime.Parse("1900-01-01 00:00:00.000");
                            }
                            else
                            {
                                InTime = DateTime.Parse(dt.Rows[i]["In Time"].ToString());
                            }

                            if (dt.Rows[i]["Out Time"].ToString() == "" || dt.Rows[i]["Out Time"].ToString() == null)
                            {
                                OutTime = DateTime.Parse("1900-01-01 00:00:00.000");
                            }
                            else
                            {
                                OutTime = DateTime.Parse(dt.Rows[i]["Out Time"].ToString());
                            }

                            DataRow dataRow = datatable.NewRow();
                            dataRow["SrNo"] = dt.Rows[i]["SrNo"];
                            dataRow["Employee Code"] = dt.Rows[i]["Employee Code"];
                            dataRow["Attedance Date"] = dt.Rows[i]["Attedance Date"];
                            dataRow["In Time"] = InTime;
                            dataRow["Out Time"] = OutTime;
                            datatable.Rows.Add(dataRow);
                        }

                        param.Add("@CreatedID", request.CreatedID);
                        param.Add("@TblAttedance", datatable.AsTableValuedParameter("TblEmployeeAttedance"));
                        param.Add("@OutResult", 0, direction: ParameterDirection.Output);

                        await SqlMapper.ExecuteAsync(_dbConnect.GetConnection(), procName, param, commandType: CommandType.StoredProcedure);
                        OutResult = param.Get<int>("@OutResult");

                        if (OutResult == 1)
                        {
                            response.Success = 1;
                            response.Message = "Attedance Add SuccessFully";
                        }
                        else
                        {
                            response.Success = 0;
                            response.Message = "Something Went Wrong To Generate Attedance";
                        }
                    }
                    else
                    {
                        response.Success = 0;
                        response.requireField = response.requireField.ToList();
                        return response;
                    }
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

        public async Task<IList<EmployeeAttedanceGet>> PayrollGetEmployeeAttedance(RequestGenerateEmployeeAttedance request)
        {
            string procName = "PayrollGetEmployeeAttedance";

            try
            {
                DynamicParameters parm = new DynamicParameters();
                parm.Add("@EmployeeCode", request.EmployeeCode);
                parm.Add("@Year", request.Year);
                parm.Add("@Month", request.Month);

                var result = await _dbConnect.GetConnection().QueryAsync<EmployeeAttedanceGet>(procName, parm, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                _dbConnect.CloseConnection();
            }

        }

        public DataTable ImportEmployeeAttedance()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SrNo", typeof(int));
            dt.Columns.Add("Employee Code", typeof(string));
            dt.Columns.Add("Attedance Date", typeof(DateTime));
            dt.Columns.Add("In Time", typeof(DateTime));
            dt.Columns.Add("Out Time", typeof(DateTime));
            return dt;
        }

    }
}
