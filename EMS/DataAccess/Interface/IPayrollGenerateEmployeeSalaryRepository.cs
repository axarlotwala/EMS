using EMS.DbConnection;
using EMS.Model;

namespace EMS.DataAccess.Interface
{
    public interface IPayrollGenerateEmployeeSalaryRepository
    {
        Task<PayrollGeneratedEmployeeSalary> PayrollGeneratedEmployeeSalary(RequestPayrollSalary request);

        Task<IList<PayrollGeneratedEmployeeSalary>> GetPayrollAllEmployeeSalary(RequestPayrollSalary request);
    }
}
