using EMS.DataAccess.Interface;
using EMS.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EMS.Controllers
{
    [Route("api/PayrollEmployee")]
    [ApiController]
    public class PayrollEmployeeController : ControllerBase
    {

        private IPayrollGenerateEmployeeSalaryRepository _repository;

        public PayrollEmployeeController(IPayrollGenerateEmployeeSalaryRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route("PayrollGeneratedEmployeeSalary")]
        public async Task<IActionResult> PayrollGeneratedEmployeeSalary([FromBody] RequestPayrollSalary request)
        {
            ApiResponse response = new ApiResponse();
            response.Success = 0;
            response.Message = "No Data Found";

            var result = await _repository.PayrollGeneratedEmployeeSalary(request);
            if (result != null)
            {
                return Ok(result);
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("GetPayrollAllEmployeeSalary")]
        public async Task<IActionResult> GetPayrollAllEmployeeSalary([FromBody] RequestPayrollSalary request)
        {
            ApiResponse response = new ApiResponse();
            response.Success = 0;
            response.Message = "No Data Found";

            var result = await _repository.GetPayrollAllEmployeeSalary(request);
            if (result != null)
            {
                return Ok(result);
            }
            return Ok(response);
        }
    }
}
