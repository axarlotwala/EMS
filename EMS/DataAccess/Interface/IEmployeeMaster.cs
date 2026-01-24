using EMS.Model;
using Microsoft.AspNetCore.Mvc;

namespace EMS.DataAccess.IRepository
{
    public interface IEmployeeMaster
    {
        Task<ApiResponse> AddEmployee(string filePath,EmployeeModel employee);
        Task<IList<BulkRegisterEmployeeResponse>> BulkRegistrationEmployee(int CreatedID,string filePath);

        Task<IList<EmployeeGetModel>> GetAllEmployee(RequestGetEmployee request);

        Task<EmployeeModelGetByID> GetEmployeeByID(RequestGetEmployee request);

        Task<ApiResponse> UpdateEmployee(int ID, string filePath, EmployeeModel request);

        Task<ApiResponse> DeleteEmployee(int ID);

        Task<AdminDashboard> AdminDashboard();
    }
}
