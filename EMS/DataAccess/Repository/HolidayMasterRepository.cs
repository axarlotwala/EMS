using Dapper;
using EMS.Common;
using EMS.DataAccess.Interface;
using EMS.DbConnection;
using EMS.Model;
using System.Data;

namespace EMS.DataAccess.Repository
{
    public class HolidayMasterRepository : IHolidayMasterRepository
    {
        
        private readonly IDbFactory _dbConnect;
        private CommonUtils commonUtils = new CommonUtils();


        public HolidayMasterRepository(IDbFactory dbFactory) { 
            _dbConnect = dbFactory;
        }
        public async Task<ApiResponse> AddHolidaysMaster(HolidayMaster holiday)
        {
            string proceName = "AddHolidayMaster";
            ApiResponse apiResponse = new ApiResponse();

            try
            {
                int OutResult = 0;

                var param = new DynamicParameters();
                param.Add("@Name", holiday.Name);
                param.Add("@CreatedID", holiday.CreatedID);
                param.Add("@OutResult", 0,direction: System.Data.ParameterDirection.Output);

                await SqlMapper.ExecuteAsync(_dbConnect.GetConnection(), proceName, param, commandType: System.Data.CommandType.StoredProcedure);
                OutResult = param.Get<int>("@OutResult");

                if (OutResult == 1)
                {
                    apiResponse.Success = 1;
                    apiResponse.Message = "Add Holiday Master SuccessFully";
                }
                else
                {
                    apiResponse.Success = 0;
                    apiResponse.Message = "Somthing Went Wrong To Add Holidays";
                }
                return apiResponse;
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

        public async Task<ApiResponse> AddBulkHoliday(string filePath)
        {
            try
            {
                ApiResponse response = new ApiResponse();
                int OutResult = 0;

                string ProcedureName = "BulkInsertHolidayMaster";
                var param = new DynamicParameters();




               
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
                        dataRow["HolidayName"] = dt.Rows[i]["HolidayName"];
                        datatable.Rows.Add(dataRow);
                    }
                }

                //param.Add("@CreatedID", CreatedID);
                param.Add("@TblBulkHolidaysName", datatable.AsTableValuedParameter("TypeHolidaysMaster"));
                param.Add("@OutResult", 0, direction: ParameterDirection.Output);

                await SqlMapper.ExecuteAsync(_dbConnect.GetConnection(), ProcedureName, param, commandType: CommandType.StoredProcedure);
                OutResult = param.Get<int>("@OutResult");

                if (OutResult == 1)
                {
                    response.Success = 1;
                    response.Message = "File Upload SuccessFully";
                }
                else
                {
                    response.Success = 0;
                    response.Message = "Something went Wrong To Upload";
                }

                return response;
                //var result = await _dbConnect.GetConnection().QueryAsync<HolidayMaster>(ProcedureName, param, commandType: CommandType.StoredProcedure);
                //return result.ToList();
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

        public async Task<ApiResponse> UpdateHolidaysMaster(int ID,HolidayMaster holiday)
        {
            string proceName = "UpdateHolidayMaster";
            ApiResponse apiResponse = new ApiResponse();

            try
            {
                int OutResult = 0;

                var param = new DynamicParameters();
                param.Add("@ID", ID);
                param.Add("@Name", holiday.Name);
                param.Add("@UpdatedID", holiday.UpdatedID);
                param.Add("@OutResult", 0, direction: System.Data.ParameterDirection.Output);

                await SqlMapper.ExecuteAsync(_dbConnect.GetConnection(), proceName, param, commandType: System.Data.CommandType.StoredProcedure);
                OutResult = param.Get<int>("@OutResult");

                if (OutResult == 1)
                {
                    apiResponse.Success = 1;
                    apiResponse.Message = "Update Holiday Master SuccessFully";
                }
                else
                {
                    apiResponse.Success = 0;
                    apiResponse.Message = "Somthing Went Wrong To Update Holidays";
                }

                return apiResponse;
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

        public async Task<ApiResponse> DeleteHolidaysMaster(int ID)
        {
            string proceName = "DeleteHolidayMaster";
            ApiResponse apiResponse = new ApiResponse();

            try
            {
                int OutResult = 0;

                var param = new DynamicParameters();
                param.Add("@ID", ID);
                param.Add("@OutResult", 0, direction: System.Data.ParameterDirection.Output);

                await SqlMapper.ExecuteAsync(_dbConnect.GetConnection(), proceName, param, commandType: System.Data.CommandType.StoredProcedure);
                OutResult = param.Get<int>("@OutResult");

                if (OutResult == 1)
                {
                    apiResponse.Success = 1;
                    apiResponse.Message = "Delete Holiday Master SuccessFully";
                }
                else
                {
                    apiResponse.Success = 0;
                    apiResponse.Message = "Somthing Went Wrong To Delete Holidays";
                }
                return apiResponse;
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

       

        public async Task<IList<GetHolidayMaster>> GetAllHolidayMasters()
        {
            try
            {
                string proceName = "GetAllHolidayMaster";

                var result = await _dbConnect.GetConnection().QueryAsync<GetHolidayMaster>(proceName, commandType: System.Data.CommandType.StoredProcedure);
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

        public async Task<GetHolidayMaster> GetHolidayMastersByID(int ID)
        {
            try
            {
                string proceName = "GetAllHolidayMaster";

                var param = new DynamicParameters();
                param.Add("@ID", ID);

                var result = await _dbConnect.GetConnection().QueryAsync<GetHolidayMaster>(proceName, param, commandType: System.Data.CommandType.StoredProcedure);
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


        #region Add Holiday Mapping Master

        public async Task<ApiResponse> AddHolidayMappingMaster(HolidayMappingMaster holiday)
        {
            string proceName = "BulkInsertHolidayMappingMaster";
            ApiResponse apiResponse = new ApiResponse();

            try
            {
                int OutResult = 0;

                var listdata = commonUtils.ToDataTable(holiday.MapData);

                var param = new DynamicParameters();
                param.Add("@TblDateWiseHolidayMapping",listdata.AsTableValuedParameter("TypeHolidayDateMapping"));
                param.Add("@CreatedID", holiday.CreatedID);
                param.Add("@OutResult", 0, direction: System.Data.ParameterDirection.Output);

                await SqlMapper.ExecuteAsync(_dbConnect.GetConnection(), proceName, param, commandType: System.Data.CommandType.StoredProcedure);
                OutResult = param.Get<int>("@OutResult");

                if (OutResult == 1)
                {
                    apiResponse.Success = 1;
                    apiResponse.Message = "Add Holiday Mapping SuccessFully";
                }
                else
                {
                    apiResponse.Success = 0;
                    apiResponse.Message = "Somthing Went Wrong To Add Holiday Mapping";
                }
                return apiResponse;
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

        public async Task<IList<GetHolidaysMapping>> GetAllHolidays()
        {
            try
            {
                string proceName = "GetAllHolidayDate";

                //var param = new DynamicParameters();
                //param.Add("@ID", ID);

                var result = await _dbConnect.GetConnection().QueryAsync<GetHolidaysMapping>(proceName, commandType: System.Data.CommandType.StoredProcedure);
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

        #endregion


        public DataTable ImportEmployee()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SrNo", typeof(int));
            dt.Columns.Add("HolidayName", typeof(string));
            return dt;
        }

        public async Task<ApiResponse> DeleteHolidayMappingByID(int ID)
        {
            string proceName = "DeleteHolidayMappingByID";
            ApiResponse apiResponse = new ApiResponse();

            try
            {
                int OutResult = 0;

                var param = new DynamicParameters();
                param.Add("@ID", ID);
                param.Add("@OutResult", 0, direction: System.Data.ParameterDirection.Output);

                await SqlMapper.ExecuteAsync(_dbConnect.GetConnection(), proceName, param, commandType: System.Data.CommandType.StoredProcedure);
                OutResult = param.Get<int>("@OutResult");

                if (OutResult == 1)
                {
                    apiResponse.Success = 1;
                    apiResponse.Message = "Delete Holiday Mapping SuccessFully";
                }
                else
                {
                    apiResponse.Success = 0;
                    apiResponse.Message = "Somthing Went Wrong To Delete Mapping";
                }
                return apiResponse;
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
