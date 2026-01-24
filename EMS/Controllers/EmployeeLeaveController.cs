using EMS.DataAccess.Interface;
using EMS.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EMS.Controllers
{
    [Route("api/Leave")]
    [ApiController]
    public class EmployeeLeaveController : ControllerBase
    {
        private IEmployeeLeaveRepository _repository;
        public EmployeeLeaveController(IEmployeeLeaveRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route("ApplyLeave")]
        public async Task<IActionResult> ApplyLeave([FromBody] RequestLeave request)
        {
            var result = await _repository.ApplyLeave(request);
            return Ok(result);
        }

        [HttpPost]
        [Route("UpdateLeave/{ID}")]
        public async Task<IActionResult> UpdateLeave(int ID,[FromBody] RequestLeave request)
        {
            var result = await _repository.UpdateLeave(ID,request);
            return Ok(result);
        }

        [HttpPost]
        [Route("EmployeeLeaveGetByID/{ID}")]
        public async Task<IActionResult> EmployeeLeaveGetByID(int ID)
        {
            ApiResponse response = new ApiResponse();
            response.Success = 0;
            response.Message = "No Data Found";


            var result = await _repository.EmployeeLeaveGetByID(ID);
            if (result != null)
            {
                return Ok(result);
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("EmployeeAllLeave")]
        public async Task<IActionResult> EmployeeAllLeave([FromBody] RequestLeave request)
        {
            ApiResponse response = new ApiResponse();
            response.Success = 0;
            response.Message = "No Data Found";


            var result = await _repository.EmployeeAllLeave(request);
            if (result.Count() > 0)
            {
                return Ok(result);
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("AdminGetAllLeave")]
        public async Task<IActionResult> AdminGetAllLeave()
        {
            ApiResponse response = new ApiResponse();
            response.Success = 0;
            response.Message = "No Data Found";


            var result = await _repository.AdminGetAllLeave();
            if (result.Count() > 0)
            {
                return Ok(result);
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("AdminUpdateLeaveStatus")]
        public async Task<IActionResult> AdminUpdateLeaveStatus([FromBody] AdminUpdateStatus request)
        {
            var result = await _repository.AdminUpdateLeaveStatus(request);
            return Ok(result);
        }
    }
}
