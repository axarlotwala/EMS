using EMS.Model;

namespace EMS.DataAccess.IRepository
{
    public interface IEmployeeMaster
    {
        Task<ApiResponse> AddEmployee(EmployeeModel employee);
        Task<IList<BulkRegisterEmployeeResponse>> BulkRegistrationEmployee(int CreatedID,string filePath);

        Task<IList<EmployeeGetModel>> GetAllEmployee();
    }
}
