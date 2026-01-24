using EMS.Model;

namespace EMS.DataAccess.Interface
{
    public interface IHolidayMasterRepository
    {
        Task<ApiResponse> AddHolidaysMaster(HolidayMaster  holiday);

        Task<ApiResponse> AddBulkHoliday(string filePath);
        Task<ApiResponse> UpdateHolidaysMaster(int ID,HolidayMaster holiday);
        Task<ApiResponse> DeleteHolidaysMaster(int ID, HolidayMaster holiday);

        Task<IList<GetHolidayMaster>> GetAllHolidayMasters();
        Task<GetHolidayMaster> GetHolidayMastersByID(int ID);

        Task<ApiResponse> AddHolidayMappingMaster(HolidayMappingMaster holiday);

        Task<IList<GetHolidaysMapping>> GetAllHolidays();

        Task<ApiResponse> DeleteHolidayMappingByID(int ID);

    }
}
