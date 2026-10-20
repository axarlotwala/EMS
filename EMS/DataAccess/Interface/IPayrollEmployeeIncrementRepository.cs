using EMS.Model;
using Microsoft.AspNetCore.Mvc;

namespace EMS.DataAccess.Interface
{
    public interface IPayrollEmployeeIncrementRepository
    {
        Task<GetEmployeeWiseFixSalary> EmployeeWiseGetFixSalary(RequestEmployeeFixSalary request);

        Task<ApiResponse> InsertPayrollEmployeeIncrement(RequestEmployeeIncrement request);
        Task<ApiResponse> UpdatePayrollEmployeeIncrement(int ID,RequestEmployeeIncrement request);
    }
}
