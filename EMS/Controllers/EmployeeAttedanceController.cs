using Azure.Core;
using EMS.DataAccess.Interface;
using EMS.Model;
using EMS.Model.Employee;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EMS.Controllers
{
    [Route("api/EmployeeAttedance")]
    [ApiController]
    public class EmployeeAttedanceController : ControllerBase
    {
        private readonly IEmployeeAttedanceRepository _repository;
        private IWebHostEnvironment _webHostEnvironment;
       

        public EmployeeAttedanceController(IEmployeeAttedanceRepository repository, IWebHostEnvironment webHostEnvironment)
        {
            _repository = repository;
            _webHostEnvironment = webHostEnvironment;

        }

        [HttpPost]
        [Route("EmployeeAttedanceGenerate")]
        public async Task<IActionResult> EmployeeAttedanceGenerate()
        {
            var formdata = Request.Form["formdata"];
            var model = JsonConvert.DeserializeObject<RequestGenerateEmployeeAttedance>(formdata);

            if (Request.Form.Files.Count > 0)
            {
                var files = Request.Form.Files[0];
                var FileName = Guid.NewGuid() + "_" + files.FileName;

                var upload = Path.Combine(_webHostEnvironment.WebRootPath, "Resources", "Upload");

                var filepath = Path.Combine(upload, FileName);

                if (!Directory.Exists(upload))
                {
                    Directory.CreateDirectory(upload);
                }

                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    await files.CopyToAsync(stream);
                    stream.Close();
                    // return Ok(FileName);
                }

                var result = await _repository.GenerateEmployeeAttedance(model,filepath);
                return Ok(result);
            }

            return Ok();
        }

        [HttpPost]
        [Route("PayrollGetEmployeeAttedance")]
        public async Task<IActionResult> PayrollGetEmployeeAttedance([FromBody] RequestGenerateEmployeeAttedance request)
        {
            ApiResponse response = new ApiResponse();
            response.Success = 0;
            response.Message = "No Data Found";

            var result = await _repository.PayrollGetEmployeeAttedance(request);
            if (result.Count() > 0)
            {
                return Ok(result);
            }

            return Ok(response);

        }
    }
}
