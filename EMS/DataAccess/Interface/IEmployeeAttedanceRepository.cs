using EMS.Model.Employee;

namespace EMS.DataAccess.Interface
{
    public interface IEmployeeAttedanceRepository
    {

        Task<BulkUploadAttedanceResponse> GenerateEmployeeAttedance(RequestGenerateEmployeeAttedance request,string filePath);

        Task<IList<EmployeeAttedanceGet>> PayrollGetEmployeeAttedance(RequestGenerateEmployeeAttedance request);


    }
}
