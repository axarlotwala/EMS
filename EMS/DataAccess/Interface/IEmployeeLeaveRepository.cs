using EMS.Model;

namespace EMS.DataAccess.Interface
{
    public interface IEmployeeLeaveRepository
    {
        Task<ApiResponse> ApplyLeave(RequestLeave request);
        Task<ApiResponse> UpdateLeave(int ID,RequestLeave request);

        Task<EmployeeLeave> EmployeeLeaveGetByID(int ID);
        Task<IList<EmployeeLeave>> EmployeeAllLeave(RequestLeave request);

        Task<IList<EmployeeLeave>> AdminGetAllLeave();
        Task<ApiResponse> AdminUpdateLeaveStatus(AdminUpdateStatus request);
    }
}
