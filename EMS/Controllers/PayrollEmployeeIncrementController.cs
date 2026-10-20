using EMS.DataAccess.Interface;
using EMS.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EMS.Controllers
{
    [Route("api/PayrollEmployeeIncrement")]
    [ApiController]
    public class PayrollEmployeeIncrementController : ControllerBase
    {
        IPayrollEmployeeIncrementRepository _repository;

        public PayrollEmployeeIncrementController(IPayrollEmployeeIncrementRepository incrementRepository)
        {
            _repository = incrementRepository;
        }

        [HttpPost]
        [Route("EmployeeWiseGetFixSalary")]
        public async Task<IActionResult> ApplyLeave([FromBody] RequestEmployeeFixSalary request)
        {
            ApiResponse response = new ApiResponse();
            response.Success = 0;
            response.Message = "No Data Found";

            var result = await _repository.EmployeeWiseGetFixSalary(request);
            if (result != null)
            {
                return Ok(result);
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("InsertPayrollEmployeeIncrement")]
        public async Task<IActionResult> PayrollEmployeeIncrement([FromBody] RequestEmployeeIncrement request)
        {
            var result = await _repository.InsertPayrollEmployeeIncrement(request);
            return Ok(result);
        }

        [HttpPost]
        [Route("UpdatePayrollEmployeeIncrement/{ID}")]
        public async Task<IActionResult> UpdatePayrollEmployeeIncrement(int ID,[FromBody] RequestEmployeeIncrement request)
        {
            var result = await _repository.UpdatePayrollEmployeeIncrement(ID,request);
            return Ok(result);
        }
    }
}
